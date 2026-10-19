using System;
using _Project.CodeBase.Configs.Player;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Infrastructure.Services.Time;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.Camera.Behaviours;
using KinematicCharacterController.Examples;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerMovement : IPlayerMovement, IInitializable, IDisposable, IFixedTickable, ITickable
    {
        public event Action<float> MoveSpeedChanged;
        public event Action Jumped;

        public bool Enabled { get; set; } = true;

        public Vector3 Velocity => _characterController != null
            ? _characterController.Motor.BaseVelocity
            : Vector3.zero;

        public Vector3 Position => _characterController != null
            ? _characterController.transform.position
            : Vector3.zero;

        public Quaternion Rotation => _characterController.IsNotNull()
            ? _characterController.transform.rotation
            : Quaternion.identity;

        public bool IsGrounded => _characterController != null &&
                                  _characterController.Motor.GroundingStatus.IsStableOnGround;

        public bool IsMoving => PlanarVelocity.sqrMagnitude >
                                MovingSpeedThreshold * MovingSpeedThreshold;

        public float MaxSpeed =>
            _config != null ? _config.MoveSpeed : 0f;

        private Vector3 PlanarVelocity
        {
            get
            {
                if (_characterController == null) return Vector3.zero;

                return Vector3.ProjectOnPlane(
                    _characterController.Motor.BaseVelocity,
                    _characterController.Motor.CharacterUp);
            }
        }

        private const float MovingSpeedThreshold = 0.05f;
        private const float MinimalStepOffset = 0.0001f;

        private readonly IInputService _inputService;
        private readonly ExampleCharacterController _characterController;
        private readonly ICameraProvider _cameraProvider;
        private readonly PlayerMovementConfig _config;
        private readonly ITimeService _timeService;

        private Vector2 _cachedMoveInput;

        private bool _pendingJump;

        public PlayerMovement(IInputService inputService, ExampleCharacterController characterController,
            ICameraProvider cameraProvider, PlayerMovementConfig config)
        {
            _inputService = inputService;
            _characterController = characterController;
            _cameraProvider = cameraProvider;
            _config = config;
        }

        public void Initialize()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents() =>
            _inputService.JumpPerformed += OnJumpRequested;

        private void UnsubscribeFromEvents() =>
            _inputService.JumpPerformed -= OnJumpRequested;

        public void Tick()
        {
            if (IsAnyHandleNull())
                return;

            ReadInput();
        }

        public void FixedTick()
        {
            if (IsAnyHandleNull())
                return;

            Move();
        }

        public void AddVelocity(Vector3 velocity)
        {
            if (_characterController == null)
                return;

            _characterController.Motor.BaseVelocity += velocity;
        }

        public void Warp(Vector3 position)
        {
            if (_characterController == null)
                return;

            _characterController.Motor.SetPosition(position);
        }

        public void FaceDirection(Vector3 direction)
        {
            if (_characterController == null)
                return;

            direction.y = 0f;

            if (direction.sqrMagnitude < MinimalStepOffset)
                return;

            _characterController.transform.rotation =
                Quaternion.LookRotation(direction.normalized, Vector3.up);
        }

        private void ReadInput()
        {
            _cachedMoveInput = _inputService.ReadMovement();
        }

        private void Move()
        {
            PlayerCharacterInputs inputs = UpdateInputs();

            _characterController.SetInputs(ref inputs);

            MoveSpeedChanged?.Invoke(PlanarVelocity.magnitude);
        }

        private PlayerCharacterInputs UpdateInputs()
        {
            PlayerCharacterInputs inputs = new PlayerCharacterInputs
            {
                MoveAxisForward = _cachedMoveInput.y,
                MoveAxisRight = _cachedMoveInput.x,
                CameraRotation = _cameraProvider.Rotation,
                JumpDown = false,
                CrouchDown = false,
                CrouchUp = false
            };

            if (_pendingJump)
            {
                if (IsGrounded)
                {
                    inputs.JumpDown = true;
                    _pendingJump = false;
                }
                else
                    _pendingJump = false;
            }

            return inputs;
        }

        private bool IsAnyHandleNull()
        {
            if (!Enabled)
                return true;

            if (_characterController == null || _cameraProvider == null || _config == null)
                return true;
            return false;
        }

        private void OnJumpRequested()
        {
            _pendingJump = true;
            Jumped?.Invoke();
        }

        public void Dispose() =>
            UnsubscribeFromEvents();
    }
}