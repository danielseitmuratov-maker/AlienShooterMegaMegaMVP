using System.Collections.Generic;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class AssemblySlotRegistry : IAssemblySlotRegistry
    {
        public IReadOnlyList<AssemblySlotView> Slots => _slots;

        private readonly List<AssemblySlotView> _slots = new List<AssemblySlotView>();

        public void Register(AssemblySlotView slot)
        {
            if (!_slots.Contains(slot))
                _slots.Add(slot);
        }

        public void Unregister(AssemblySlotView slot) => 
            _slots.Remove(slot);

        public AssemblySlotView Find(string slotId)
        {
            for (int i = 0; i < _slots.Count; i++)
                if (_slots[i].SlotId == slotId)
                    return _slots[i];
            
            return null;
        }
    }
}