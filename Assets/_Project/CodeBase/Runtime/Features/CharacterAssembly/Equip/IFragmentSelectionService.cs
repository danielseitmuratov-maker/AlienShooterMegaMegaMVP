using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip
{
    public interface IFragmentSelectionService
    {
        FragmentInstance SelectedFragment { get; }
        string TargetSlotId { get; }
        bool HasSelection { get; }

        event Action Changed;

        void SelectFragment(FragmentInstance fragment);
        void SelectSlot(string slotId);
        void Clear();
    }
}