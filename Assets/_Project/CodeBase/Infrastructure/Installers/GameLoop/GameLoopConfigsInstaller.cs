using _Project.CodeBase.Common.Attributes;
using _Project.CodeBase.Configs.Game;
using _Project.CodeBase.Runtime.Features.Camera.Behaviours;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Presentation;
using _Project.CodeBase.Runtime.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers.GameLoop
{
    [CreateAssetMenu(fileName = "GameLoopConfigsInstaller", menuName = "Installers/Create GameLoopConfigsInstaller")]
    public class GameLoopConfigsInstaller : ScriptableObjectInstaller<GameLoopConfigsInstaller>
    {
        [Required(nameof(GameLoopConfigsInstaller) + " game loop config is null")] [SerializeField]
        private GameLoopConfig _gameLoopConfig;

        [Required(nameof(GameLoopConfigsInstaller) + " player root config is null")] [SerializeField]
        private PlayerRootConfig _playerRootConfig;

        [Required(nameof(GameLoopConfigsInstaller) + " camera follow config is null")] [SerializeField]
        private CameraFollowConfig _cameraFollow;

        [Required(nameof(GameLoopConfigsInstaller) + "assembly character config is null")] [SerializeField]
        private AssemblyCharacterConfig _assemblyCharacter;

        [Required(nameof(GameLoopConfigsInstaller) + "assembly equip animations config is null")] [SerializeField]
        private EquipAnimationConfigsConfig _equipAnimationConfigs;
        
        [Required(nameof(GameLoopConfigsInstaller) + "starting fragments config is null")] [SerializeField]
        private StartingFragmentsConfig _startingFragmentsConfig;


        public override void InstallBindings()
        {
            BindSingleConfig(_gameLoopConfig);
            BindSingleConfig(_playerRootConfig);
            BindSingleConfig(_cameraFollow);
            BindSingleConfig(_assemblyCharacter);
            BindSingleConfig(_equipAnimationConfigs);
            BindSingleConfig(_startingFragmentsConfig);
        }

        private void BindSingleConfig<TConfig>(TConfig config) where TConfig : ScriptableObject =>
            Container.Bind<TConfig>().FromInstance(config).AsSingle();
    }
}