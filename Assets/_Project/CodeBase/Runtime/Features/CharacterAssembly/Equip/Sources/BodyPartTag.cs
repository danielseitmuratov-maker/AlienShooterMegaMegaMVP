using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources
{
    public sealed class BodyPartTag : MonoBehaviour
    {
        [field: SerializeField] public string SlotId { get; private set; }
    }
}