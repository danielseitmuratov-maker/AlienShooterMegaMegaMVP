using System;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerAnimator : IPlayerAnimator, IInitializable, IDisposable, ITickable
    {
        public bool Enabled { get; set; } = true;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int JumpHash = Animator.StringToHash("Jump");

        private readonly Animator _animator;
        private readonly IPlayerMovement _movement;

        public PlayerAnimator(Animator animator, IPlayerMovement movement)
        {
            _animator = animator;
            _movement = movement;
        }

        public void Initialize()
        {
            _movement.Jumped += OnJumped;
        }
        

        public void Tick()
        {
            if (!Enabled)
                return;

            _animator.SetFloat(SpeedHash, GetHorizontalSpeed());
        }

        private float GetHorizontalSpeed()
        {
            Vector3 velocity = _movement.Velocity;
            return new Vector3(velocity.x, 0f, velocity.z).magnitude;
        }

        private void OnJumped()
        {
            _animator.SetTrigger(JumpHash);
            Debug.Log("animator on jumped");
        }
        
        public void Dispose()
        {
            _movement.Jumped -= OnJumped;
        }
    }
}