using System;
using _Project.CodeBase.Runtime.Common.Extensions;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService, IDisposable
    {
        public event Action AttackPerformed;
        public event Action JumpPerformed;
        public event Action JumpCancelled;
        public event Action CrouchPerformed;
        public event Action CrouchCanceled;

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
            _input.Player.Jump.performed += context => JumpPerformed?.Invoke();
            _input.Player.Jump.canceled += context => JumpCancelled?.Invoke();
            _input.Player.Crouch.performed += context => CrouchPerformed?.Invoke();
            _input.Player.Crouch.canceled += context => CrouchCanceled?.Invoke();
            _input.Player.Attack.performed += context => AttackPerformed?.Invoke();
        }
        

        public Vector2 ReadMovement()
        {
            if (_input.IsNotNull())
                _input.Player.Move.ReadValue<Vector2>();

            Debug.LogError($"Error while reading player movement input from {this} class");

            return Vector2.zero;
        }

        public void Dispose()
        {
            _input?.Disable();
            _input?.Dispose();
        }
    }
}