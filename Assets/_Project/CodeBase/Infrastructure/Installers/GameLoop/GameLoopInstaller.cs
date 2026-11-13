using _Project.CodeBase.Infrastructure.GameStates;
using _Project.CodeBase.Runtime.Features.Camera.Behaviours;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Events;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Factories;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Presentation;
using _Project.CodeBase.Runtime.Features.Levels;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers.GameLoop
{
    public class GameLoopInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            BindAssemblyFeatures();
            
            BindFactories();
            
            BindGameStateMachine();
            BindLevelController();
            BindPlayerProvider();
            BindCamera();

            BindPlayerSpawner();
        }


        private void BindAssemblyFeatures()
        {
            Container.Bind<ICharacterAssembler>().To<CharacterAssembler>().AsSingle();
            Container.Bind<ICharacterDisassembler>().To<CharacterDisassembler>().AsSingle();
            Container.Bind<ICharacterAssemblyEvents>().To<CharacterAssemblyEvents>().AsSingle();
            Container.Bind<IFragmentEquipService>().To<FragmentEquipService>().AsSingle();
            Container.Bind<IFragmentSelectionService>().To<FragmentSelectionService>().AsSingle();
            Container.Bind<IFragmentInstanceFactory>().To<FragmentInstanceFactory>().AsSingle();
            Container.Bind<IStartingFragmentsProvider>().To<StartingFragmentsProvider>().AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<FragmentEquipAnimator>()
                .AsSingle()
                .NonLazy();
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
                .WithArguments(_camera);

            Container
                .BindInterfacesAndSelfTo<CameraFollowController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerSpawner() => 
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().AsSingle();
    }
}