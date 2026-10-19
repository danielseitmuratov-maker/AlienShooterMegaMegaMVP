using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Doors
{
    public class DoorNode
    {
        public DoorSide Side;
        public Vector2Int Position;
        public int CorridorId;
        public bool IsLocked;
        public bool IsSecret;
    }
}