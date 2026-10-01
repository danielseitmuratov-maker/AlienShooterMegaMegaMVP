using _Project.CodeBase.Runtime.Features.Camera;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using KinematicCharacterController.Examples;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private ExampleCharacterController _controller;
        [SerializeField] private Transform _cameraTransform;

        public override void InstallBindings()
        {
            BindCoreComponents();
            BindFacade();
        }

        private void BindFacade()
        {
            Container.BindInterfacesAndSelfTo<PlayerFacade>().AsSingle();
        }

        private void BindCoreComponents()
        {
            Container.Bind<ExampleCharacterController>().FromInstance(_controller).AsSingle();
            
            Container
                .Bind<ICameraProvider>()
                .To<CameraProvider>()
                .AsSingle()
                .WithArguments(_cameraTransform);

            Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
            Container.Bind<IPlayerAnimator>().To<PlayerAnimator>().AsSingle();
        }
    }
}