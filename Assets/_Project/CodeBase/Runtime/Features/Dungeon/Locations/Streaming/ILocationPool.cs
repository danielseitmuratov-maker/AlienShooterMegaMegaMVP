using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public interface ILocationPool
    {
        void Tick(Vector3 playerPosition, IReadOnlyList<RoomInstance> rooms);
    }
}