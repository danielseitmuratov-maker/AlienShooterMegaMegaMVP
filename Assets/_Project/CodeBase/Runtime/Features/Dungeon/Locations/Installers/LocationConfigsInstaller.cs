using _Project.CodeBase.Runtime.Features.Dungeon.Location;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Installers
{
    [CreateAssetMenu(fileName = "LocationConfigsInstaller", menuName = "Installers/LocationConfigsInstaller")]
    public class LocationConfigsInstaller : ScriptableObjectInstaller<LocationConfigsInstaller>
    {
        [SerializeField] private LocationPrefabsConfig _locationPrefabs;
        [SerializeField] private LocationPoolConfig _pool;
        [SerializeField] private DungeonGenerationConfig _dungeonGeneration;
        

        public override void InstallBindings()
        {
            BindSingle(_locationPrefabs);
            BindSingle(_pool);
            BindSingle(_dungeonGeneration);
        }
        
        private void BindSingle<TConfig>(TConfig config) where TConfig : ScriptableObject => 
            Container.Bind<TConfig>().FromInstance(config).AsSingle();
    }
}