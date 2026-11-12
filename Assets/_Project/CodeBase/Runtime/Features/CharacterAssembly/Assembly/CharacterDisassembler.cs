using System;
using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly
{
    public sealed class CharacterDisassembler : ICharacterDisassembler
    {
        public event Action OnOperationStarted;
        public event Action<CharacterInstance> OnOperationDone;
        public event Action<CharacterInstance> OnOperationFailed;

        public UniTask<IReadOnlyList<FragmentInstance>> DisAssembleAsync(CharacterInstance character,
            CancellationToken token)
        {
            OnOperationStarted?.Invoke();

            try
            {
                if (!ValidateInput(character))
                {
                    OnOperationFailed?.Invoke(character);
                    return EmptyResult();
                }

                IReadOnlyList<FragmentInstance> fragments = CollectFragments(character, token);

                DestroyVisuals(character);
                DestroySkeleton(character);
                character.Clear();

                OnOperationDone?.Invoke(character);
                return UniTask.FromResult(fragments);
            }
            catch (OperationCanceledException)
            {
                OnOperationFailed?.Invoke(character);
                throw;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                OnOperationFailed?.Invoke(character);
                return EmptyResult();
            }
        }

        private static IReadOnlyList<FragmentInstance> CollectFragments(
            CharacterInstance character,
            CancellationToken token)
        {
            List<FragmentInstance> fragments = new List<FragmentInstance>(character.Equipment.Count);

            foreach (var kvp in character.Equipment)
            {
                token.ThrowIfCancellationRequested();
                fragments.Add(kvp.Value);
            }

            return fragments;
        }

        private static void DestroyVisuals(CharacterInstance character)
        {
            foreach (var visual in character.Visuals.Values)
                if (visual != null)
                    UnityEngine.Object.Destroy(visual);
        }

        private static void DestroySkeleton(CharacterInstance character)
        {
            if (character.SkeletonRoot != null)
                UnityEngine.Object.Destroy(character.SkeletonRoot.gameObject);
        }

        private static bool ValidateInput(CharacterInstance character)
        {
            if (character == null)
            {
                Debug.LogError("[CharacterDisassembler] Character is null.");
                return false;
            }

            if (character.SkeletonRoot == null)
            {
                Debug.LogWarning(
                    "[CharacterDisassembler] SkeletonRoot is null — nothing to destroy.");
            }

            return true;
        }

        private static UniTask<IReadOnlyList<FragmentInstance>> EmptyResult() =>
            UniTask.FromResult<IReadOnlyList<FragmentInstance>>(Array.Empty<FragmentInstance>());
    }
}