using System;
using _Project.CodeBase.Configs.Player;
using UnityEngine;
using Zenject;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.Camera;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class PlayerMovement : ITickable, IInitializable, IDisposable, IPlayerMovement
    {
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value) 
                    return;
                _enabled = value;
                if (!value)
                    ResetInputs();
            }
        }

        public Vector3 Velocity => _motor.Velocity;
        public Vector3 Position => _motor.TransientPosition;
        public Quaternion Rotation => _motor.TransientRotation;
        public bool IsGrounded => _motor.GroundingStatus.IsStableOnGround;
        public bool IsMoving => _motor.Velocity.sqrMagnitude > Constants.MoveThreshold;
        public float MaxSpeed => _maxSpeed;
        
        private readonly ExampleCharacterController _controller;
        private readonly KinematicCharacterMotor _motor;
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly PlayerMovementConfig _config;

        private bool _enabled = true;
        private bool _jumpDown;
        private bool _crouchUp;
        private bool _crouchHeld;

        private Vector2 _lastMoveInput;
        private Quaternion _lastCameraRotation;
        private bool _hasSentInputs;

        private PlayerCharacterInputs _cachedInputs;
        private float _maxSpeed;

        public PlayerMovement(ExampleCharacterController controller, IInputService inputService,
            ICameraProvider cameraProvider, PlayerMovementConfig config)
        {
            _controller = controller;
            _motor = controller.Motor;
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _config = config;
        }

        public void Initialize()
        {
            InitializeStartValues();
            SubscribeToEvents();
        }

        private void InitializeStartValues()
        {
            _maxSpeed = _config.MaxMoveSped;
        }

        private void SubscribeToEvents()
        {
            if (_inputService.IsNotNull())
            {
                _inputService.JumpPerformed += OnJumpPerformed;
                _inputService.CrouchPerformed += OnCrouchPerformed;
                _inputService.CrouchCanceled += OnCrouchCanceled;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (_inputService.IsNotNull())
            {
                _inputService.JumpPerformed -= OnJumpPerformed;
                _inputService.CrouchPerformed -= OnCrouchPerformed;
                _inputService.CrouchCanceled -= OnCrouchCanceled;
            }
        }

        public void Tick()
        {
            UpdateMoveInput();
        }

        public void AddVelocity(Vector3 velocity)
        {
            if (!_enabled)
                return;

            _controller.AddVelocity(velocity);
        }

        public void Warp(Vector3 position)
        {
            _motor.SetPosition(position);

            _hasSentInputs = false;
        }

        public void FaceDirection(Vector3 direction)
        {
            if (direction.sqrMagnitude < Constants.MoveThreshold)
                return;

            direction.y = 0f;

            _motor.SetRotation(Quaternion.LookRotation(direction.normalized));
        }

        private void UpdateMoveInput()
        {
            Vector2 moveInput = _enabled ? _inputService.ReadMovement() : Vector2.zero;
            Quaternion camRotation = _cameraProvider?.Rotation ?? Quaternion.identity;

            bool hasOneShot = _jumpDown || _crouchUp;

            if (HasSentInputs(hasOneShot, moveInput, camRotation))
                return;

            RefreshCachedInputs(moveInput, camRotation);

            _controller.SetInputs(ref _cachedInputs);

            _lastMoveInput = moveInput;
            _lastCameraRotation = camRotation;
            _hasSentInputs = true;

            _jumpDown = false;
            _crouchUp = false;
        }

        private bool HasSentInputs(bool hasOneShot, Vector2 moveInput, Quaternion camRotation) =>
            !hasOneShot && _hasSentInputs && moveInput == _lastMoveInput && camRotation == _lastCameraRotation;

        private void RefreshCachedInputs(Vector2 moveInput, Quaternion camRotation)
        {
            _cachedInputs.MoveAxisForward = moveInput.y;
            _cachedInputs.MoveAxisRight = moveInput.x;
            _cachedInputs.CameraRotation = camRotation;
            _cachedInputs.JumpDown = _jumpDown;
            _cachedInputs.CrouchDown = _crouchHeld;
            _cachedInputs.CrouchUp = _crouchUp;
        }

        private void OnJumpPerformed() =>
            _jumpDown = true;

        private void OnCrouchPerformed() =>
            _crouchHeld = true;

        private void OnCrouchCanceled()
        {
            _crouchHeld = false;
            _crouchUp = true;
        }

        private void ResetInputs()
        {
            _jumpDown = false;
            _crouchHeld = false;
            _crouchUp = false;
            _hasSentInputs = false;
        }

        public void Dispose() => UnsubscribeFromEvents();
    }
}
