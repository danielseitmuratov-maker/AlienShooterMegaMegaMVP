using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public sealed class LocationCleaner : ILocationCleaner
    {
        public void CleanUp(IReadOnlyList<RoomInstance> rooms)
        {
            if (rooms == null) return;

            for (int i = 0; i < rooms.Count; i++)
            {
                RoomBasic root = rooms[i].Root;
                if (root != null)
                    Object.Destroy(root);
            }
        }
    }
}