using _Project.CodeBase.Configs.Player;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Factory
{
    public class PlayerAsyncFactory : IPlayerAsyncFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly PlayerRootConfig _config;
        private IPlayerAsyncFactory _playerAsyncFactoryImplementation;


        public PlayerAsyncFactory(IInstantiator instantiator, PlayerRootConfig config)
        {
            _instantiator = instantiator;
            _config = config;
        }

        public async UniTask<IPlayerFacade> Create(Vector3 at, Quaternion startRotation, Transform parent = null)
        {
            PlayerFacade player =
                _instantiator.InstantiatePrefabForComponent<PlayerFacade>(_config.Prefab, at, startRotation, parent);

            await player.InitializeAsync();

            return player;
        }
    }
}