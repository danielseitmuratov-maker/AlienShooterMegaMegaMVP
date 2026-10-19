using _Project.CodeBase.Runtime.Features.Dungeon.Location;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Placement;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStreamingServices();
            BindBuildingsServices();
            BindGenerationServices();
            BindPlacementServices();
            BindLocationController();
        }


        private void BindStreamingServices()
        {
            Container.Bind<ILocationWarmUpper>().To<LocationWarmUpper>().AsSingle();
            Container.Bind<ILocationCleaner>().To<LocationCleaner>().AsSingle();
            Container.Bind<ILocationPool>().To<LocationPool>().AsSingle();
            Container.Bind<IDungeonStreamer>().To<DungeonStreamer>().AsSingle();
        }
        
        private void BindBuildingsServices()
        {
            Container.Bind<ILocationProvider>().To<LocationProvider>().AsSingle();
            Container.Bind<ILocationBuilder>().To<LocationBuilder>().AsSingle();
        }

        private void BindGenerationServices()
        {
            BindLayoutStrategy();
            Container.Bind<IDungeonGenerator>().To<DungeonGenerator>().AsSingle();
        }

        private void BindLayoutStrategy() => 
            Container.Bind<IDungeonLayoutStrategy>().To<BspLayoutStrategy>().AsSingle();

        private void BindPlacementServices() => 
            Container.Bind<IPlayerDungeonPlacement>().To<PlayerDungeonPlacement>().AsSingle();

        private void BindLocationController()
        {
            Container
                .BindInterfacesAndSelfTo<LocationController>()
                .AsSingle()
                .NonLazy();
        }
    }
}