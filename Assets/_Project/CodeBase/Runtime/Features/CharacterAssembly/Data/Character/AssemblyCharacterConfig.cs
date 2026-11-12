using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character
{
    [CreateAssetMenu(fileName = "AssemblyCharacterConfig", menuName = "SO/" + nameof(AssemblyCharacterConfig), order = 0)]
    public class AssemblyCharacterConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public List<SlotDefinition> Slots { get; private set; }
    }
}