using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Components
{
    public class ConnectionBasic : MonoBehaviour
    {
        [field: SerializeField] public IReadOnlyList<Transform> Entrances { get; private set; }
    }
}