using _Project.CodeBase.Configs.Game;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.GameStates.States
{
    public class GameLoopState : IGameState
    {
        private readonly IPlayerAsyncFactory _asyncFactory;
        private readonly GameLoopConfig _config;

        private IPlayerFacade _player;

        public GameLoopState(IPlayerAsyncFactory asyncFactory, GameLoopConfig config)
        {
            _asyncFactory = asyncFactory;
            _config = config;
        }

        public async UniTask Enter()
        {
           _player = await SpawnPlayer();
        }

        private async UniTask<IPlayerFacade> SpawnPlayer()
        {
            Quaternion startPlayerRotation = Quaternion.Euler(_config.StartPlayerRotation);
            
            IPlayerFacade playerFacade = await _asyncFactory.Create(_config.StartPlayerPosition, startPlayerRotation);
            return playerFacade;
        }

        public async UniTask Exit()
        {
        }
    }
}