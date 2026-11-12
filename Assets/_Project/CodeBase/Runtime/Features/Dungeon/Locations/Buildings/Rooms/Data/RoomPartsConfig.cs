using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings.Rooms.Data
{
    [CreateAssetMenu(fileName = "RoomPartsConfig", menuName = "Configs/Dungeon/Room Parts")]
    public sealed class RoomPartsConfig : ScriptableObject
    {
        [Serializable]
        public struct RoomPartsEntry
        {
            public RoomType Type;

            [Header("Geometry")]
            public GameObject FloorPrefab;
            public GameObject CeilingPrefab;
            public List<GameObject> WallPrefabs;      
            public List<GameObject> CornerPrefabs;   
            public GameObject DoorwayPrefab;    

            [Header("Decor")]
            public List<DecorEntry> DecorPrefabs;   

            [Header("Points")]
            public GameObject SpawnPointPrefab;
            public GameObject VfxPointPrefab;
        }

        [Serializable]
        public struct DecorEntry
        {
            public GameObject Prefab;
            [Range(0f, 1f)] public float Weight;
        }

        public List<RoomPartsEntry> Rooms = new List<RoomPartsEntry>();

        public RoomPartsEntry GetParts(RoomType type) => 
            new RoomPartsEntry();
    }
}