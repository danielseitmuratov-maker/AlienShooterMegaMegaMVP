using System;
using _Project.CodeBase.Configs.Camera;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Camera.Behaviours
{
    public class CameraFollowController : IInitializable, IDisposable, ILateTickable
    {
        private readonly ICameraProvider _cameraProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly CameraFollowConfig _config;

        private Vector3 _velocity;

        public CameraFollowController(ICameraProvider cameraProvider, IPlayerProvider playerProvider, CameraFollowConfig config)
        {
            _cameraProvider = cameraProvider;
            _playerProvider = playerProvider;
            _config = config;
        }


        public void Initialize() => 
            SubscribeToEvents();

        private void SubscribeToEvents() => 
            _playerProvider.Changed += OnPlayerChanged;

        private void UnsubscribeFromEvents() => 
            _playerProvider.Changed -= OnPlayerChanged;

        public void LateTick()
        {
            IPlayerFacade player = _playerProvider.Current;
            
            if (player == null)
                return;

            Vector3 target = player.CameraFollowPointTransform.position + _config.Offset;

            _cameraProvider.Transform.position = Vector3.SmoothDamp(
                _cameraProvider.Transform.position, target, ref _velocity, _config.SmoothTime);
        }

        private void OnPlayerChanged(IPlayerFacade player) => 
            RefreshVelocity();

        private void RefreshVelocity() => 
            _velocity = Vector3.zero;

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}