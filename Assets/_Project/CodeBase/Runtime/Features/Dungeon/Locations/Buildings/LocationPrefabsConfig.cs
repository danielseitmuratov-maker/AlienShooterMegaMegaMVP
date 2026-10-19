using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings
{
    [CreateAssetMenu(
        fileName = "LocationPrefabsConfig",
        menuName = "Configs/Dungeon/Location Prefabs")]
    public class LocationPrefabsConfig : ScriptableObject
    {
        [Serializable]
        public struct RoomPrefabEntry
        {
            public RoomType Type;
            public RoomBasic Prefab;
        }

        [field: SerializeField] public float CellSize { get; private set; }
        [field: SerializeField] public List<RoomPrefabEntry> Rooms { get; private set; }
    }
}