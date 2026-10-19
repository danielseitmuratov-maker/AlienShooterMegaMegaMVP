using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public sealed class LocationPool : ILocationPool
    {
        private readonly float _loadSqr;
        private readonly float _unloadSqr;

        public LocationPool(LocationPoolConfig config)
        {
            _loadSqr = config.LoadRadius * config.LoadRadius;
            _unloadSqr = config.UnloadRadius * config.UnloadRadius;
        }

        public void Tick(Vector3 playerPosition, IReadOnlyList<RoomInstance> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                RoomInstance room = rooms[i];
                float sqr = (room.Center - playerPosition).sqrMagnitude;

                if (!room.IsActive && sqr <= _loadSqr)
                    room.SetActive(true);
                else if (room.IsActive && sqr >= _unloadSqr)
                    room.SetActive(false);
            }
        }
    }
}