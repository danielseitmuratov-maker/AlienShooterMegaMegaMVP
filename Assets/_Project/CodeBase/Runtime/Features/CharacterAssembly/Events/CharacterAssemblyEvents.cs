using System;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Events
{
    public sealed class CharacterAssemblyEvents : ICharacterAssemblyEvents
    {
        public event Action<FragmentEquippedArgs> FragmentEquipped;
        public event Action<FragmentUnequippedArgs> FragmentUnequipped;

        public void RaiseFragmentEquipped(FragmentEquippedArgs args) =>
            FragmentEquipped?.Invoke(args);

        public void RaiseFragmentUnequipped(FragmentUnequippedArgs args) =>
            FragmentUnequipped?.Invoke(args);
    }
}