using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public interface IDungeonStreamer
    {
        void SetRooms(IReadOnlyList<RoomInstance> rooms);
        void Tick(Vector3 playerPosition);
        void Warmup(Vector3 playerPosition);
        void Clear();
    }
}

