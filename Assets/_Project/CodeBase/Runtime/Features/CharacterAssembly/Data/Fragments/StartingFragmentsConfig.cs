using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments
{
    [CreateAssetMenu(fileName = "StartingFragmentsConfig", menuName = "SO/" + nameof(StartingFragmentsConfig),
        order = 0)]
    public class StartingFragmentsConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<FragmentDefinitionConfig> Fragments { get; private set; } = new List<FragmentDefinitionConfig>();
    }
}