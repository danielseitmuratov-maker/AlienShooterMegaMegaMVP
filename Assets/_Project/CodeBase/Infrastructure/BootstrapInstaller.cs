using _Project.CodeBase.Infrastructure.GameStates;
using _Project.CodeBase.Infrastructure.GameStates.States;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Infrastructure.Services.Time;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindGameStates();
            BindGameStateMachine();
            BindApplicationController();
        }

        private void BindInfrastructureServices()
        {
            BindTimeService();
            BindInputServiceCommon();
            BindFactories();
        }
        
        
        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindGameStates()
        {
           // Container
           //     .Bind<IGameState>()
           //     .WithId(GameStateType.Bootstrap)
           //     .To<BootstrapState>()
           //     .AsSingle();
            
            Container
                .Bind<IGameState>()
                .WithId(GameStateType.GameLoop)
                .To<GameLoopState>()
                .AsSingle();
        }

        private void BindApplicationController()
        {
            Container
                .BindInterfacesAndSelfTo<ApplicationController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindTimeService() =>
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();

        private void BindInputServiceCommon()
        {
            if (Application.isMobilePlatform)
                Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
            else
                Container.Bind<IInputService>().To<PcInputService>().AsSingle();
        }
        
        private void BindFactories()
        {
            Container.Bind<IPlayerAsyncFactory>().To<PlayerAsyncFactory>().AsSingle();
        }
    }
}