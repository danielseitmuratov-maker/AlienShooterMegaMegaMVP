using System;
using _Project.CodeBase.Runtime.Common.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        public event Action AttackPerformed;
        public event Action JumpPerformed;
        public event Action JumpCancelled;
        public event Action CrouchPerformed;
        public event Action CrouchCanceled;
        public event Action EquipFragmentPerformed;
        public event Action EquipFragmentCancelled;
        public event Action ClickPerformed;

        public Vector3 PointerPosition =>
            Pointer.current != null ? (Vector3) Pointer.current.position.ReadValue() : Vector3.zero;

        private GameInput _input;

        public MobileInputService()
        {
            InitializeGameInput();
            SubscribeToEvents();
        }

        private void InitializeGameInput()
        {
            _input = new GameInput();
            _input.Enable();
        }

        private void SubscribeToEvents()
        {
            if (_input.IsNull()) return;

            _input.Player.Jump.performed += context => JumpPerformed?.Invoke();
            _input.Player.Jump.canceled += context => JumpCancelled?.Invoke();
            _input.Player.Crouch.performed += context => CrouchPerformed?.Invoke();
            _input.Player.Crouch.canceled += context => CrouchCanceled?.Invoke();
            _input.Player.Attack.performed += context => AttackPerformed?.Invoke();

            _input.Player.EquipFragment.performed += context => EquipFragmentPerformed?.Invoke();
            _input.Player.EquipFragment.canceled += context => EquipFragmentCancelled?.Invoke();

            _input.Player.Click.performed += context => ClickPerformed?.Invoke();
        }

        public Vector2 ReadMovement()
        {
            if (_input.IsNotNull())
                return _input.Player.Move.ReadValue<Vector2>();

            Debug.LogError($"[MobileInputService] Cannot read movement.");
            return Vector2.zero;
        }

        public Vector2 ReadZoom()
        {
            if (_input.IsNotNull())
                return _input.Player.Zoom.ReadValue<Vector2>();

            Debug.LogError($"[MobileInputService] Cannot read zoom.");
            return Vector2.zero;
        }

        public Vector2 ReadCameraRotation()
        {
            if (_input.IsNotNull())
                return _input.Player.CameraRotation.ReadValue<Vector2>();

            Debug.LogError($"[MobileInputService] Cannot read camera rotation.");
            return Vector2.zero;
        }

        public void Dispose()
        {
            _input?.Disable();
            _input?.Dispose();
        }
    }
}