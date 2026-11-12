using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentBehaviourDataConfig", menuName = "SO/" + nameof(FragmentBehaviourDataConfig), order = 0)]
    public abstract class FragmentBehaviourDataConfig : ScriptableObject
    {
        [field: SerializeField] public string BehaviorId { get; private set; }
    }
}