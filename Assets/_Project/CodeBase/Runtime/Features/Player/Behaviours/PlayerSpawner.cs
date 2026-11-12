using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Components;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using _Project.CodeBase.Runtime.Features.Player.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class PlayerSpawner : IPlayerSpawner
    {
        private readonly IPlayerAsyncFactory _playerFactory;
        private readonly ICharacterAssembler _characterAssembler;
        private readonly ICharacterDisassembler _characterDisassembler;
        private readonly IPlayerProvider _playerProvider;

        public PlayerSpawner(IPlayerAsyncFactory playerFactory, ICharacterAssembler characterAssembler,
            ICharacterDisassembler characterDisassembler, IPlayerProvider playerProvider)
        {
            _playerFactory = playerFactory;
            _characterAssembler = characterAssembler;
            _characterDisassembler = characterDisassembler;
            _playerProvider = playerProvider;
        }

        public async UniTask<IPlayerFacade> SpawnAsync(AssemblyCharacterConfig config,
            IReadOnlyList<FragmentInstance> fragments, Vector3 position, Quaternion rotation, CancellationToken token)
        {
            IPlayerFacade player = null;
            CharacterInstance character = null;

            try
            {
                player = await _playerFactory.Create(Vector3.zero, rotation);
                if (player == null)
                {
                    Debug.LogError("[PlayerSpawner] Failed to create player facade.");
                    return null;
                }

                token.ThrowIfCancellationRequested();

                CharacterRootBone skeleton =
                    player.Transform.GetComponentInChildren<CharacterRootBone>(true);

                if (skeleton == null)
                {
                    Debug.LogError(
                        "[PlayerSpawner] No CharacterRootBone found in player prefab.");
                    DestroyPlayer(player);
                    return null;
                }

                character = await _characterAssembler.AssembleAsync(
                    config,
                    skeleton.transform,
                    fragments,
                    token);

                if (character == null)
                {
                    Debug.LogError("[PlayerSpawner] Failed to assemble character.");
                    DestroyPlayer(player);
                    return null;
                }

                player.SetCharacter(character);

                await player.TeleportAsync(position, 0f);

                _playerProvider.Set(player);

                return player;
            }
            catch
            {
                if (character?.SkeletonRoot != null)
                    UnityEngine.Object.Destroy(character.SkeletonRoot.gameObject);
                DestroyPlayer(player);
                throw;
            }
        }

        public async UniTask DespawnAsync(IPlayerFacade player, CancellationToken token)
        {
            if (player == null)
                return;

            _playerProvider.Clear();

            if (player.Character != null)
                await _characterDisassembler.DisAssembleAsync(player.Character, token);

            DestroyPlayer(player);
        }

        private static void DestroyPlayer(IPlayerFacade player)
        {
            if (player?.Transform != null)
                UnityEngine.Object.Destroy(player.Transform.gameObject);
        }
    }
}