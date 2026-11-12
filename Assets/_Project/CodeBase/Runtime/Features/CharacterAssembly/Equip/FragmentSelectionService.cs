using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip
{
    public sealed class FragmentSelectionService : IFragmentSelectionService
    {
        public FragmentInstance SelectedFragment { get; private set; }
        public string TargetSlotId { get; private set; }

        public bool HasSelection =>
            SelectedFragment != null && !string.IsNullOrEmpty(TargetSlotId);

        public event Action Changed;

        public void SelectFragment(FragmentInstance fragment)
        {
            SelectedFragment = fragment;
            Changed?.Invoke();
        }

        public void SelectSlot(string slotId)
        {
            TargetSlotId = slotId;
            Changed?.Invoke();
        }

        public void Clear()
        {
            SelectedFragment = null;
            TargetSlotId = null;
            Changed?.Invoke();
        }
    }
}