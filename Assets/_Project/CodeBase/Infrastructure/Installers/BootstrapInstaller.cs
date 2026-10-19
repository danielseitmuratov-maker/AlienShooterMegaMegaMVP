using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Infrastructure.Services.Random;
using _Project.CodeBase.Infrastructure.Services.Time;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
        }

        private void BindInfrastructureServices()
        {
            BindTimeService();
            BindInputServiceCommon();
            BindRandomService();
        }

        private void BindRandomService()
        {
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
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
    }
}   