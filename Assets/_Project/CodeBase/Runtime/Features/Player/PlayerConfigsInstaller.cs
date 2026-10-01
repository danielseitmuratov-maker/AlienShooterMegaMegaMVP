using _Project.CodeBase.Configs.Player;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player
{
    public class PlayerConfigsInstaller : ScriptableObjectInstaller<PlayerConfigsInstaller>
    {
        [SerializeField] private PlayerMovementConfig _movement;
        
        
        public override void InstallBindings()
        {
            BindBasicConfigs();
        }

        private void BindBasicConfigs()
        {
            Container.Bind<PlayerMovementConfig>().FromInstance(_movement).AsSingle();
        }
    }
}