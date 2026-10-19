using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations;
using _Project.CodeBase.Runtime.Features.Dungeon.Rooms;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings
{
    public sealed class LocationBuilder : ILocationBuilder
    {
        private const int RoomsPerFrame = 8;
        private const float RoomBoundsHeight = 10f;

        private readonly ILocationProvider _provider;
        private readonly IInstantiator _instantiator;

        public LocationBuilder(ILocationProvider provider, IInstantiator instantiator)
        {
            _provider = provider;
            _instantiator = instantiator;
        }

        public async UniTask<List<RoomInstance>> Build(DungeonGenerationContext context, CancellationToken token)
        {
            if (context == null || context.Layout == null)
            {
                Debug.LogError("[LocationBuilder] Context or Layout is null.");
                return new List<RoomInstance>();
            }

            var layout = context.Layout;
            var random = context.Random;
            float cellSize = _provider.CellSize;

            var result = new List<RoomInstance>(layout.Rooms.Count);

            for (int i = 0; i < layout.Rooms.Count; i++)
            {
                token.ThrowIfCancellationRequested();

                var node = layout.Rooms[i];
                var prefab = _provider.GetRoomPrefab(node.Type);

                if (prefab == null)
                {
                    Debug.LogError(
                        $"[LocationBuilder] Skip room {node.Id} — no prefab for {node.Type}.");
                    continue;
                }

                Vector3 worldCenter = GridToWorld(node, cellSize);

                RoomBasic room = _instantiator.InstantiatePrefabForComponent<RoomBasic>(
                    prefab,
                    worldCenter,Quaternion.identity,null);

                if (room == null)
                {
                    Debug.LogError($"[LocationBuilder] Failed to instantiate room {node.Id}.");
                    continue;
                }

                room.name = $"Room_{node.Id}_{node.Type}";
                room.gameObject.SetActive(false);

                Vector3 worldSize = new Vector3(
                    node.GridSize.x * cellSize,
                    RoomBoundsHeight,
                    node.GridSize.y * cellSize);

                result.Add(new RoomInstance
                {
                    RoomId = node.Id,
                    Type = node.Type,
                    Center = worldCenter,
                    Bounds = new Bounds(worldCenter, worldSize),
                    Root = room,
                    IsActive = false
                });

                if (i % RoomsPerFrame == RoomsPerFrame - 1)
                    await UniTask.Yield();
            }

            return result;
        }

        private static Vector3 GridToWorld(RoomNode node, float cellSize)
        {
            return new Vector3(
                (node.GridPosition.x + node.GridSize.x * 0.5f) * cellSize,
                0f,
                (node.GridPosition.y + node.GridSize.y * 0.5f) * cellSize);
        }
    }
}