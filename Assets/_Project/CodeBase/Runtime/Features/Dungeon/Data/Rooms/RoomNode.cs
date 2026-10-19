using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Doors;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms
{
    public class RoomNode
    {
        public int Id;
        public RoomType Type;
        public Vector2Int GridPosition;
        public Vector2Int GridSize;
        public int Depth;
        public List<int> Connections;
        public List<DoorNode> Doors;

        public RoomNode()
        {
            Connections = new List<int>();
            Doors = new List<DoorNode>();
        }
    }
}