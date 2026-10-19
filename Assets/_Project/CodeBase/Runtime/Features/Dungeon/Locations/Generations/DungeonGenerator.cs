using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Infrastructure.Services.Random;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations
{
    public sealed class DungeonGenerator : IDungeonGenerator
    {
        private readonly IDungeonLayoutStrategy _layoutStrategy;
        private readonly ILocationBuilder _builder;
        private readonly ILocationCleaner _cleaner;
        private readonly DungeonGenerationConfig _config;
        private readonly IRandomService _randomService;

        public DungeonGenerator(IDungeonLayoutStrategy layoutStrategy, ILocationBuilder builder,
            ILocationCleaner cleaner, DungeonGenerationConfig config, IRandomService randomService)
        {
            _layoutStrategy = layoutStrategy;
            _builder = builder;
            _cleaner = cleaner;
            _config = config;
            _randomService = randomService;
        }

        public async UniTask<DungeonGenerationResult> GenerateAsync(CancellationToken token)
        {
            int seed = _randomService.Range(int.MinValue, int.MaxValue);
            Debug.Log($"current seed : {seed}");
            DungeonGenerationContext context = new DungeonGenerationContext(seed, _config);

            _layoutStrategy.Create(context);
            
            if (!IsValidLayout(context.Layout))
                return null;

            List<RoomInstance> rooms = await _builder.Build(context, token);
            
            if (!IsValidRooms(rooms))
                return null;

            RoomInstance startRoom = FindStartRoom(rooms, context.Layout.StartRoomId);
            if (!IsValidStartRoom(startRoom, context.Layout))
                return null;

            return new DungeonGenerationResult
            {
                Layout = context.Layout,
                Rooms = rooms,
                StartRoom = startRoom,
            };
        }

        private bool IsValidLayout(DungeonLayout layout)
        {
            if (layout == null || layout.Rooms == null || layout.Rooms.Count == 0)
            {
                Debug.LogError("[DungeonGenerator] Layout is empty.");
                return false;
            }

            return true;
        }

        private bool IsValidRooms(List<RoomInstance> rooms)
        {
            if (rooms == null || rooms.Count == 0)
            {
                Debug.LogError("[DungeonGenerator] No rooms built.");
                return false;
            }

            return true;
        }

        private bool IsValidStartRoom(RoomInstance startRoom, DungeonLayout layout)
        {
            if (startRoom == null)
            {
                Debug.LogError(
                    $"[DungeonGenerator] Start room {layout.StartRoomId} not found.");
                return false;
            }

            return true;
        }

        private RoomInstance FindStartRoom(List<RoomInstance> rooms, int startRoomId)
        {
            for (int i = 0; i < rooms.Count; i++)
                if (rooms[i].RoomId == startRoomId)
                    return rooms[i];

            return null;
        }
    }
}