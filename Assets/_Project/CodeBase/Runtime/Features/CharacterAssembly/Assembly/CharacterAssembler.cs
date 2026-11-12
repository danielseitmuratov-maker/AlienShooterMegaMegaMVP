using System;
using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly
{
    public sealed class CharacterAssembler : ICharacterAssembler
    {
        public event Action OnAssembleOperationStarted;
        public event Action<CharacterInstance> OnAssembleOperationDone;
        public event Action<CharacterInstance> OnAssembleOperationFailed;

        private const int YieldInterval = 4;

        private readonly IInstantiator _instantiator;

        public CharacterAssembler(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public async UniTask<CharacterInstance> AssembleAsync(
            AssemblyCharacterConfig assemblyConfig,
            Transform skeletonRoot,
            IReadOnlyList<FragmentInstance> fragments,
            CancellationToken token)
        {
            OnAssembleOperationStarted?.Invoke();

            if (!ValidateInput(assemblyConfig, skeletonRoot, fragments))
            {
                OnAssembleOperationFailed?.Invoke(null);
                return null;
            }

            var character = new CharacterInstance(assemblyConfig, skeletonRoot);

            try
            {
                await AttachFragmentsAsync(assemblyConfig, character, fragments, token);

                OnAssembleOperationDone?.Invoke(character);
                return character;
            }
            catch (OperationCanceledException)
            {
                OnAssembleOperationFailed?.Invoke(character);
                throw;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                OnAssembleOperationFailed?.Invoke(character);
                return null;
            }
        }

        public async UniTask<bool> AttachFragmentAsync(
            CharacterInstance character,
            FragmentInstance fragment,
            string slotId,
            CancellationToken token)
        {
            if (character == null || fragment?.DefinitionConfig == null)
                return false;

            SlotDefinition? slot = FindSlotById(character.Config, slotId);
            if (slot == null)
            {
                Debug.LogError($"[CharacterAssembler] Slot '{slotId}' not found.");
                return false;
            }

            if (character.IsSlotOccupied(slotId))
            {
                Debug.LogWarning($"[CharacterAssembler] Slot '{slotId}' is occupied.");
                return false;
            }

            if (!fragment.DefinitionConfig.IsCompatibleWith(slot.Value.SlotType))
            {
                Debug.LogWarning(
                    $"[CharacterAssembler] Fragment '{fragment.DefinitionConfig.Id}' " +
                    $"is not compatible with slot '{slotId}' ({slot.Value.SlotType}).");
                return false;
            }

            token.ThrowIfCancellationRequested();

            return AttachVisual(character, fragment, slot.Value, slotId);
        }

        public UniTask<FragmentInstance> DetachFragmentAsync(
            CharacterInstance character,
            string slotId,
            CancellationToken token)
        {
            if (character == null)
                return UniTask.FromResult<FragmentInstance>(null);

            token.ThrowIfCancellationRequested();

            if (!character.TryRemoveEquipment(slotId, out FragmentInstance fragment, out GameObject visual))
            {
                Debug.LogWarning($"[CharacterAssembler] Slot '{slotId}' is empty.");
                return UniTask.FromResult<FragmentInstance>(null);
            }

            if (visual != null)
                UnityEngine.Object.Destroy(visual);

            return UniTask.FromResult(fragment);
        }

        private async UniTask AttachFragmentsAsync(
            AssemblyCharacterConfig config,
            CharacterInstance character,
            IReadOnlyList<FragmentInstance> fragments,
            CancellationToken token)
        {
            for (int i = 0; i < fragments.Count; i++)
            {
                token.ThrowIfCancellationRequested();

                FragmentInstance fragment = fragments[i];
                if (fragment?.DefinitionConfig == null)
                    continue;

                SlotDefinition? slot = FindFreeCompatibleSlot(config, character, fragment);
                if (slot == null)
                {
                    Debug.LogWarning(
                        $"[CharacterAssembler] No free compatible slot for " +
                        $"fragment '{fragment.DefinitionConfig.Id}'.");
                    continue;
                }

                AttachVisual(character, fragment, slot.Value, slot.Value.SlotId);

                if (i % YieldInterval == YieldInterval - 1)
                    await UniTask.Yield();
            }
        }

        private bool AttachVisual(
            CharacterInstance character,
            FragmentInstance fragment,
            SlotDefinition slot,
            string slotId)
        {
            if (!character.Bones.TryGetValue(slot.BoneName, out Transform bone))
            {
                Debug.LogError(
                    $"[CharacterAssembler] Bone '{slot.BoneName}' not found " +
                    $"for slot '{slotId}'.");
                return false;
            }

            if (fragment.DefinitionConfig.Mesh == null)
            {
                Debug.LogWarning(
                    $"[CharacterAssembler] Fragment '{fragment.DefinitionConfig.Id}' has no Mesh.");
                return false;
            }

            GameObject visual = _instantiator.InstantiatePrefab(fragment.DefinitionConfig.Mesh, bone);
            visual.transform.localPosition = slot.AttachOffset;
            visual.transform.localRotation = Quaternion.Euler(slot.AttachEuler);

            character.AddEquipment(slotId, fragment, visual);
            return true;
        }

        private static SlotDefinition? FindSlotById(AssemblyCharacterConfig config, string slotId)
        {
            for (int i = 0; i < config.Slots.Count; i++)
            {
                if (config.Slots[i].SlotId == slotId)
                    return config.Slots[i];
            }

            return null;
        }

        private static SlotDefinition? FindFreeCompatibleSlot(
            AssemblyCharacterConfig config,
            CharacterInstance character,
            FragmentInstance fragment)
        {
            List<SlotType> compatible = fragment.DefinitionConfig.CompatibleSlots;
            if (compatible == null || compatible.Count == 0)
                return null;

            for (int i = 0; i < config.Slots.Count; i++)
            {
                SlotDefinition slot = config.Slots[i];

                if (!compatible.Contains(slot.SlotType))
                    continue;

                if (character.IsSlotOccupied(slot.SlotId))
                    continue;

                return slot;
            }

            return null;
        }

        private static bool ValidateInput(
            AssemblyCharacterConfig config,
            Transform skeletonRoot,
            IReadOnlyList<FragmentInstance> fragments)
        {
            if (config == null)
            {
                Debug.LogError("[CharacterAssembler] Config is null.");
                return false;
            }

            if (skeletonRoot == null)
            {
                Debug.LogError("[CharacterAssembler] SkeletonRoot is null.");
                return false;
            }

            if (fragments == null || fragments.Count == 0)
                Debug.LogWarning("[CharacterAssembler] No fragments to assemble.");

            return true;
        }
    }
}