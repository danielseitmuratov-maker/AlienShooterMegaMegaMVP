using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Events
{
    public readonly struct FragmentEquippedArgs
    {
        public readonly CharacterInstance Character;
        public readonly FragmentInstance Fragment;
        public readonly string SlotId;
        public readonly GameObject Visual;

        public FragmentEquippedArgs(
            CharacterInstance character,
            FragmentInstance fragment,
            string slotId,
            GameObject visual)
        {
            Character = character;
            Fragment = fragment;
            SlotId = slotId;
            Visual = visual;
        }
    }
}