using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerAnimator : IPlayerAnimator, ITickable
    {
        public bool Enabled { get; set; } = true;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        private static readonly int GroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int LandHash = Animator.StringToHash("Land");

        private const float SpeedSmoothTime = 0.1f;
        private const float AirborneJumpThreshold = 0.1f;
        private const float FallVelocityThreshold = -0.1f;

        private readonly Animator _animator;
        private readonly IPlayerMovement _movement;

        private float _smoothedSpeed;
        private float _speedDamp;
        private bool _wasGrounded;

        public PlayerAnimator(Animator animator, IPlayerMovement movement)
        {
            _animator = animator;
            _movement = movement;
            _wasGrounded = movement.IsGrounded;
        }

        public void Tick()
        {
            if (!Enabled)
                return;

            Vector3 smoothVelocity = SmoothOutMovementVelocity();

            bool grounded = _movement.IsGrounded;

            _animator.SetFloat(SpeedHash, _smoothedSpeed);
            _animator.SetFloat(VerticalSpeedHash, smoothVelocity.y);
            _animator.SetBool(GroundedHash, grounded);

            if (IsJumping(grounded, smoothVelocity))
                _animator.SetTrigger(JumpHash);

            if (IsLanding(grounded, smoothVelocity))
                _animator.SetTrigger(LandHash);

            _wasGrounded = grounded;
        }

        private Vector3 SmoothOutMovementVelocity()
        {
            Vector3 velocity = _movement.Velocity;

            Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
            float normalized = _movement.MaxSpeed > 0.001f
                ? Mathf.Clamp01(horizontal.magnitude / _movement.MaxSpeed)
                : 0f;

            _smoothedSpeed = Mathf.SmoothDamp(_smoothedSpeed, normalized, ref _speedDamp, SpeedSmoothTime);
            return velocity;
        }

        private bool IsLanding(bool grounded, Vector3 velocity) =>
            grounded && !_wasGrounded && velocity.y < FallVelocityThreshold;

        private bool IsJumping(bool grounded, Vector3 velocity) =>
            !grounded && _wasGrounded && velocity.y > AirborneJumpThreshold;
    }
}