using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;

        public override void InstallBindings()
        {
            BindKccComponents();
            BindCoreComponents();
            BindFacade();
        }

        private void BindKccComponents()
        {
            Container
                .BindInterfacesAndSelfTo<KinematicCharacterMotor>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ExampleCharacterController>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindFacade()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerFacade>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }

        private void BindCoreComponents()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerMovement>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerAnimator>()
                .AsSingle()
                .WithArguments(_animator);

            Container
                .Bind<IPlayerTeleporter>()
                .To<PlayerTeleporter>()
                .AsSingle();
        }
    }
}