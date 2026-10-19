using _Project.CodeBase.Configs.Game;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.GameStates.States
{
    public class GameLoopState : IGameState
    {
        private readonly IPlayerAsyncFactory _playerAsyncFactory;
        private readonly IPlayerProvider _playerProvider;
        private readonly GameLoopConfig _config;

        private IPlayerFacade _player;

        public GameLoopState(IPlayerAsyncFactory playerAsyncFactory, IPlayerProvider playerProvider,
            GameLoopConfig config)
        {
            _playerAsyncFactory = playerAsyncFactory;
            _playerProvider = playerProvider;
            _config = config;
        }

        public async UniTask Enter()
        {
            _player = await SpawnPlayer(); 
            await _player.TeleportAsync(_config.StartPlayerPosition);
        }

        private async UniTask<IPlayerFacade> SpawnPlayer()
        {
            Quaternion startPlayerRotation = Quaternion.Euler(_config.StartPlayerRotation);

            IPlayerFacade playerFacade =
                await _playerAsyncFactory.Create(Vector3.zero, startPlayerRotation);

            _playerProvider.Set(playerFacade);

            return playerFacade;
        }

        public async UniTask Exit()
        {
            _playerProvider.Clear();
            await UniTask.CompletedTask;
        }
    }
}