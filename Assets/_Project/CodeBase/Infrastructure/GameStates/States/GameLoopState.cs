using System;
using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Configs.Game;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.GameStates.States
{
    public class GameLoopState : IGameState
    {
        private readonly IPlayerSpawner _playerSpawner;
        private readonly AssemblyCharacterConfig _assemblyConfig;
        private readonly GameLoopConfig _config;

        private IPlayerFacade _player;
        private CancellationTokenSource _cts;

        public GameLoopState(IPlayerSpawner playerSpawner, AssemblyCharacterConfig assemblyConfig,
            GameLoopConfig config)
        {
            _playerSpawner = playerSpawner;
            _assemblyConfig = assemblyConfig;
            _config = config;
        }

        public async UniTask Enter()
        {
            _cts = new CancellationTokenSource();

            try
            {
                Quaternion rotation = Quaternion.Euler(_config.StartPlayerRotation);
                IReadOnlyList<FragmentInstance> fragments = GetStartingFragments();

                _player = await _playerSpawner.SpawnAsync(
                    _assemblyConfig,
                    fragments,
                    _config.StartPlayerPosition,
                    rotation,
                    _cts.Token);

                if (_player.IsNull())
                    Debug.LogError("[GameLoopState] Player spawn failed.");
            }
            catch (OperationCanceledException)
            {
                _player = null;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _player = null;
            }
        }

        public async UniTask Exit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            if (_player.IsNotNull())
            {
                await _playerSpawner.DespawnAsync(_player, default);
                _player = null;
            }
        }

        private IReadOnlyList<FragmentInstance> GetStartingFragments() => 
            Array.Empty<FragmentInstance>();
    }
}