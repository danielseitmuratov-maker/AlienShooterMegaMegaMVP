using _Project.CodeBase.Infrastructure.GameStates;
using _Project.CodeBase.Runtime.Features.Camera.Behaviours;
using _Project.CodeBase.Runtime.Features.Levels;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers.GameLoop
{
    public class GameLoopInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraTransform;

        public override void InstallBindings()
        {
            BindFactories();
            BindGameStateMachine();
            BindLevelController();
            BindPlayerProvider();
            BindCamera();
        }

        private void BindLevelController()
        {
            Container
                .BindInterfacesAndSelfTo<LevelController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<IPlayerAsyncFactory>().To<PlayerAsyncFactory>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindPlayerProvider() =>
            Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();

        private void BindCamera()
        {
            Container
                .Bind<ICameraProvider>()
                .To<CameraProvider>()
                .AsSingle()
                .WithArguments(_cameraTransform);

            Container
                .BindInterfacesAndSelfTo<CameraFollowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}