using _Project.CodeBase.Runtime.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player
{
    [CreateAssetMenu(fileName = "PlayerConfigsInstaller", menuName = "Installers/PlayerConfigsInstaller")]
    public class PlayerConfigsInstaller : ScriptableObjectInstaller<PlayerConfigsInstaller>
    {
        [SerializeField] private PlayerMovementConfig _movement;

        public override void InstallBindings()
        {
            BindBasicConfigs();
        }

        private void BindBasicConfigs()
        {
            BindSingle(_movement);
        }

        private void BindSingle<TConfig>(TConfig config) where TConfig : ScriptableObject => 
            Container.Bind<TConfig>().FromInstance(config).AsSingle();
    }
}