using System;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Events
{
    public interface ICharacterAssemblyEvents
    {
        event Action<FragmentEquippedArgs> FragmentEquipped;
        event Action<FragmentUnequippedArgs> FragmentUnequipped;

        void RaiseFragmentEquipped(FragmentEquippedArgs args);
        void RaiseFragmentUnequipped(FragmentUnequippedArgs args);
    }
}