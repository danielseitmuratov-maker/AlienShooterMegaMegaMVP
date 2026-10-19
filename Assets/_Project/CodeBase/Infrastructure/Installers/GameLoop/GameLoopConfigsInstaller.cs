using _Project.CodeBase.Common.Attributes;
using _Project.CodeBase.Configs.Camera;
using _Project.CodeBase.Configs.Game;
using _Project.CodeBase.Configs.Player;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers.GameLoop
{
    [CreateAssetMenu( fileName = "GameLoopConfigsInstaller", menuName = "Installers/Create GameLoopConfigsInstaller")]
    public class GameLoopConfigsInstaller : ScriptableObjectInstaller<GameLoopConfigsInstaller>
    {
        [Required(nameof(GameLoopConfigsInstaller) + " game loop config is null")]
        [SerializeField] private GameLoopConfig _gameLoopConfig;
        
        [Required(nameof(GameLoopConfigsInstaller) + " player root config is null")]
        [SerializeField] private PlayerRootConfig _playerRootConfig;
        
        [Required(nameof(GameLoopConfigsInstaller) + " camera follow config is null")]
        [SerializeField] private CameraFollowConfig _cameraFollow;
        

        public override void InstallBindings()
        {
            BindSingleConfig(_gameLoopConfig);
            BindSingleConfig(_playerRootConfig);
            BindSingleConfig(_cameraFollow);
        }

        private void BindSingleConfig<TConfig>(TConfig config) where TConfig : ScriptableObject => 
            Container.Bind<TConfig>().FromInstance(config).AsSingle();
    }
}