using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings
{
    public class LocationProvider : ILocationProvider
    {
        public float CellSize => _cellSize;
        public bool HasPrefabFor(RoomType type) => _prefabs.ContainsKey(type);

        private readonly Dictionary<RoomType, RoomBasic> _prefabs = new Dictionary<RoomType, RoomBasic>();
        
        private LocationPrefabsConfig _config;
        private float _cellSize;

        public LocationProvider(LocationPrefabsConfig config)
        {
            _config = config;
            _cellSize = _config.CellSize;
            GetRoomsFromConfig(_config);
        }

        public RoomBasic GetRoomPrefab(RoomType type)
        {
            if (_prefabs.TryGetValue(type, out var prefab))
                return prefab;

            Debug.LogError($"[LocationProvider] No prefab registered for room type {type}.");
            return null;
        }
        
        private void GetRoomsFromConfig(LocationPrefabsConfig config)
        {
            if (config.IsNotNull())
            {
                for (int i = 0; i < config.Rooms.Count; i++)
                {
                    LocationPrefabsConfig.RoomPrefabEntry entry = config.Rooms[i];
                    _prefabs[entry.Type] = entry.Prefab;
                }
            }
        }
    }
}