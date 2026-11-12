using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments
{
    [Serializable]
    public struct SlotDefinition
    {
        [field: SerializeField] public string SlotId { get; private set; }
        [field: SerializeField] public SlotType SlotType { get; private set; }
        [field: SerializeField] public string BoneName { get; private set; }
        [field: SerializeField] public Vector3 AttachOffset { get; private set; }
        [field: SerializeField] public Vector3 AttachEuler { get; private set; }
    }
}