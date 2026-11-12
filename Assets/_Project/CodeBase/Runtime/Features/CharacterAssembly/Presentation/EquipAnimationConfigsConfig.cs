using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Presentation
{
    [CreateAssetMenu(fileName = "EquipAnimationConfigsConfig", menuName = "SO/" + nameof(EquipAnimationConfigsConfig), order = 0)]
    public class EquipAnimationConfigsConfig : ScriptableObject
    {
        [field: SerializeField] public List<EquipAnimationConfig> Animations { get; private set; } = new List<EquipAnimationConfig>();

        public EquipAnimationConfig Get(FragmentType type)
        {
            for (int i = 0; i < Animations.Count; i++)
                if (Animations[i] != null && Animations[i].Type == type)
                    return Animations[i];
            
            return null;
        }
    }
}
