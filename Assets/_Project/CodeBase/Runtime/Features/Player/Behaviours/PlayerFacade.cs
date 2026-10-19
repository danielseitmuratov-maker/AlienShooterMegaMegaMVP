using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerFacade : MonoBehaviour, IPlayerFacade
    {
        public Transform Transform => transform;

        public Transform CameraFollowPointTransform =>
            _cameraFollowPointTransform != null ? _cameraFollowPointTransform : transform;

        [SerializeField] private Transform _cameraFollowPointTransform;

        private bool _isInitialized;

        private IPlayerTeleporter _teleporter;

        [Inject]
        public void Construct(IPlayerTeleporter teleporter)
        {
            _teleporter = teleporter;
        }


        public async UniTask InitializeAsync()
        {
            CheckForDoneInitialization();
            await UniTask.CompletedTask;
        }


        public async UniTask TeleportAsync(Vector3 to, float delay = 0.5f) =>
            await _teleporter.TeleportAsync(to, delay);

        private void CheckForDoneInitialization()
        {
            if (_isInitialized)
                throw new InvalidOperationException($"{name} already initialized");

            _isInitialized = true;
        }
    }
}