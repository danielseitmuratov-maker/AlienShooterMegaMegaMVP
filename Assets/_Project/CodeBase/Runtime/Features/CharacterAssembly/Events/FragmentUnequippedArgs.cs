using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Events
{
    public readonly struct FragmentUnequippedArgs
    {
        public readonly CharacterInstance Character;
        public readonly FragmentInstance Fragment;
        public readonly string SlotId;

        public FragmentUnequippedArgs(
            CharacterInstance character,
            FragmentInstance fragment,
            string slotId)
        {
            Character = character;
            Fragment = fragment;
            SlotId = slotId;
        }
    }
}