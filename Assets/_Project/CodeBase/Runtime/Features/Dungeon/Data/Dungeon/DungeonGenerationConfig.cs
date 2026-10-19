using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon
{
    [CreateAssetMenu(
        fileName = "DungeonGenerationConfig",
        menuName = "Configs/Dungeon/Generation")]
    public class DungeonGenerationConfig : ScriptableObject
    {
        [field: SerializeField] public LayoutAlgorithmType Algorithm { get; private set; }
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: SerializeField] public int MinRooms { get; private set; }
        [field: SerializeField] public int MaxRooms { get; private set; }
    }
}