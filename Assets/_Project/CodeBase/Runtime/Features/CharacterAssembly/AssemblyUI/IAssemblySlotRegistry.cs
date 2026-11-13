using System.Collections.Generic;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public interface IAssemblySlotRegistry
    {
        IReadOnlyList<AssemblySlotView> Slots { get; }
        void Register(AssemblySlotView slot);
        void Unregister(AssemblySlotView slot);
        AssemblySlotView Find(string slotId);
    }
}