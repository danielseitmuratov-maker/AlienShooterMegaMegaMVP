using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.Random;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout
{
    internal sealed class BspNode
    {
        public RectInt Area;
        public BspNode Left;
        public BspNode Right;
        public RoomNode Room;  

        public bool IsLeaf => Left == null && Right == null;

        public RoomNode GetRandomLeafRoom(IRandomService rand)
        {
            List<RoomNode> rooms = GetAllLeafRooms();
            return rooms.Count > 0 ? rooms[rand.Next(rooms.Count)] : null;
        }

        public List<RoomNode> GetAllLeafRooms()
        {
            List<RoomNode> result = new List<RoomNode>();
            Collect(result);
            return result;
        }

        private void Collect(List<RoomNode> rooms)
        {
            if (IsLeaf)
            {
                if (Room != null) rooms.Add(Room);
                return;
            }

            Left?.Collect(rooms);
            Right?.Collect(rooms);
        }
    }
}