using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Presentation
{
    [CreateAssetMenu(fileName = "EquipAnimationConfig", menuName = "SO/" + nameof(EquipAnimationConfig), order = 0)]
    public class EquipAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public FragmentType Type { get; private set; }
        [field: SerializeField] public float Duration { get; private set; } = 0.3f;
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.OutBack;
        [field: SerializeField] public Vector3 StartScale { get; private set; } = Vector3.zero;
    }
}