using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Events;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip
{
    public sealed class FragmentEquipService : IFragmentEquipService
    {
        private readonly ICharacterAssembler _assembler;
        private readonly ICharacterAssemblyEvents _events;

        public FragmentEquipService(ICharacterAssembler assembler, ICharacterAssemblyEvents events)
        {
            _assembler = assembler;
            _events = events;
        }

        public async UniTask<bool> EquipAsync(CharacterInstance character, FragmentInstance fragment, string slotId,
            CancellationToken token)
        {
            if (character == null || fragment?.DefinitionConfig == null)
                return false;

            if (fragment.IsBroken)
            {
                Debug.LogWarning($"[FragmentEquipService] Fragment is broken.");
                return false;
            }

            if (!fragment.DefinitionConfig.IsCompatibleWith(SlotTypeOf(character, slotId)))
            {
                Debug.LogWarning($"[FragmentEquipService] Incompatible fragment.");
                return false;
            }

            if (character.IsSlotOccupied(slotId))
            {
                FragmentInstance removed = await _assembler.DetachFragmentAsync(character, slotId, token);

                if (removed != null)
                    _events.RaiseFragmentUnequipped(
                        new FragmentUnequippedArgs(character, removed, slotId));
            }

            bool attached = await _assembler.AttachFragmentAsync(character, fragment, slotId, token);

            if (!attached)
                return false;

            if (character.TryGetVisual(slotId, out GameObject visual))
                _events.RaiseFragmentEquipped(
                    new FragmentEquippedArgs(character, fragment, slotId, visual));

            return true;
        }

        public async UniTask<FragmentInstance> UnEquipAsync(CharacterInstance character, string slotId,
            CancellationToken token)
        {
            FragmentInstance removed = await _assembler.DetachFragmentAsync(character, slotId, token);

            if (removed != null)
                _events.RaiseFragmentUnequipped(
                    new FragmentUnequippedArgs(character, removed, slotId));

            return removed;
        }

        private static SlotType SlotTypeOf(CharacterInstance character, string slotId)
        {
            var slots = character.Config.Slots;

            for (int i = 0; i < slots.Count; i++)
                if (slots[i].SlotId == slotId)
                    return slots[i].SlotType;

            return SlotType.None;
        }
    }
}