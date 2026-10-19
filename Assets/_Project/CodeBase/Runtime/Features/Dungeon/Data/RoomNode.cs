using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data
{
    public class RoomNode
    {
        public int Id;
        public RoomType Type;
        public Vector2Int GridPosition; 
        public Vector2Int GridSize;
        public int Depth;
        public List<int> Connections;
    }
}