using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public sealed class DungeonStreamer : IDungeonStreamer
    {
        private readonly ILocationPool _pool;
        private readonly ILocationWarmUpper _warmup;

        private IReadOnlyList<RoomInstance> _rooms;

        public DungeonStreamer(ILocationPool pool, ILocationWarmUpper warmup)
        {
            _pool = pool;
            _warmup = warmup;
        }

        public void SetRooms(IReadOnlyList<RoomInstance> rooms)
        {
            _rooms = rooms;
        }

        public void Tick(Vector3 playerPosition)
        {
            if (_rooms == null || _rooms.Count == 0)
                return;

            _pool.Tick(playerPosition, _rooms);
        }

        public void Warmup(Vector3 playerPosition)
        {
            if (_rooms == null || _rooms.Count == 0)
                return;

            _warmup.WarmUp(playerPosition, _rooms);
        }

        public void Clear()
        {
            _rooms = null;
        }
    }
}
