using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerFacade : MonoBehaviour, IPlayerFacade 
    {
        public Transform Transform => transform;

        private bool _isInitialized;

        public async UniTask InitializeAsync()
        {
            CheckForDoneInitialization();
            await UniTask.CompletedTask;
        }

        
        private void CheckForDoneInitialization()
        {
            if (_isInitialized)
                throw new InvalidOperationException($"{name} already initialized");

            _isInitialized = true;
        }
    }
}