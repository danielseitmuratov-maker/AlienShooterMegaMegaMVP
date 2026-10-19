using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    [CreateAssetMenu(
        fileName = "LocationPoolConfig",
        menuName = "Configs/Dungeon/Location Pool")]
    public class LocationPoolConfig : ScriptableObject
    {
        [field: SerializeField] public float LoadRadius { get; private set; }
        [field: SerializeField] public float UnloadRadius { get; private set; }
        [field: SerializeField] public int WarmupRooms { get; private set; }
    }
}