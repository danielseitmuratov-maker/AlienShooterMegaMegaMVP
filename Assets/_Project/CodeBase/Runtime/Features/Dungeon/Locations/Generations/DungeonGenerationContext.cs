using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.Random;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using _Project.CodeBase.Runtime.Features.Dungeon.Location;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations
{
    public sealed class DungeonGenerationContext
    {
        public int Seed { get; }
        public DungeonGenerationConfig Config { get; }
        public IRandomService Random { get; }
        public DungeonLayout Layout { get; }

        public Vector2Int GridSize => Config.GridSize;

        public DungeonGenerationContext(int seed, DungeonGenerationConfig config)
        {
            Seed = seed;
            Config = config;

            Random = new SeededRandomService(seed);

            Layout = new DungeonLayout
            {
                Seed = seed,
                Rooms = new List<RoomNode>(),
                Connections = new List<Connection>(),
                StartRoomId = -1,
            };
        }
    }
}