using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public sealed class LocationWarmUpper : ILocationWarmUpper
    {
        private readonly LocationPoolConfig _config;

        public LocationWarmUpper(LocationPoolConfig config)
        {
            _config = config;
        }

        public void WarmUp(Vector3 playerPosition, IReadOnlyList<RoomInstance> rooms)
        {
            if (rooms == null || rooms.Count == 0) 
                return;

            List<RoomInstance> sorted = new List<RoomInstance>(rooms);
            
            sorted
                .Sort((a, b) => (a.Center - playerPosition).sqrMagnitude
                .CompareTo((b.Center - playerPosition).sqrMagnitude));

            int count = Mathf.Min(_config.WarmupRooms, sorted.Count);

            for (int i = 0; i < count; i++)
                sorted[i].SetActive(true);

            for (int i = count; i < sorted.Count; i++)
                sorted[i].SetActive(false);
        }
    }
}
