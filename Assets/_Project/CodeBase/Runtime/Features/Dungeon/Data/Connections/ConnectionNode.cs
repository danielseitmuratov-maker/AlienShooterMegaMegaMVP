using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Connections
{
    public class ConnectionNode
    {
        public int Id;
        public ConnectionShape Shape;
        public ConnectionType Type;
        public List<int> ConnectedRoomIds;
        public List<Vector2Int> ConnectedPoints; 
        public List<Vector2Int> Path;
        public int RotationIndex;          
    }
}