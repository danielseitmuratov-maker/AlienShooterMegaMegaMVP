using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.Random;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout
{
    public sealed class BspLayoutStrategy : IDungeonLayoutStrategy
    {
        public LayoutAlgorithmType Type => LayoutAlgorithmType.Bsp;

        private const int MinLeafSize = 8;
        private const int MaxDepth = 5;
        private const int RoomPadding = 2;
        private const float LootChance = 0.25f;

        public DungeonLayout Create(DungeonGenerationContext context)
        {
            var ctx = new BspContext(context.Random, context.Layout);

            var rootRect = new RectInt(0, 0, context.GridSize.x, context.GridSize.y);
            var root = SplitRecursive(ctx, rootRect, 0);

            ConnectRecursive(ctx, root);
            AssignStartRoom(ctx);
            AssignDepths(root, 0);

            return context.Layout;
        }

        private BspNode SplitRecursive(BspContext ctx, RectInt rect, int depth)
        {
            var node = new BspNode { Area = rect };

            if (depth >= MaxDepth
                || rect.width < MinLeafSize * 2
                || rect.height < MinLeafSize * 2)
            {
                node.Room = CreateRoom(ctx, rect);
                ctx.Layout.Rooms.Add(node.Room);
                return node;
            }

            bool splitVertical = ChooseSplitDirection(ctx, rect);

            if (splitVertical)
            {
                int splitX = ctx.Random.Range(
                    rect.x + rect.width / 3,
                    rect.x + rect.width * 2 / 3);

                splitX = Mathf.Clamp(splitX, rect.x + MinLeafSize, rect.xMax - MinLeafSize);

                var left = new RectInt(rect.x, rect.y, splitX - rect.x, rect.height);
                var right = new RectInt(splitX, rect.y, rect.xMax - splitX, rect.height);

                node.Left = SplitRecursive(ctx, left, depth + 1);
                node.Right = SplitRecursive(ctx, right, depth + 1);
            }
            else
            {
                int splitY = ctx.Random.Range(
                    rect.y + rect.height / 3,
                    rect.y + rect.height * 2 / 3);

                splitY = Mathf.Clamp(splitY, rect.y + MinLeafSize, rect.yMax - MinLeafSize);

                var top = new RectInt(rect.x, rect.y, rect.width, splitY - rect.y);
                var bottom = new RectInt(rect.x, splitY, rect.width, rect.yMax - splitY);

                node.Left = SplitRecursive(ctx, top, depth + 1);
                node.Right = SplitRecursive(ctx, bottom, depth + 1);
            }

            return node;
        }

        private bool ChooseSplitDirection(BspContext ctx, RectInt rect)
        {
            float ratio = (float)rect.width / rect.height;
            if (ratio > 1.25f) return true;
            if (ratio < 0.8f) return false;
            return ctx.Random.Next(2) == 0;
        }

        private RoomNode CreateRoom(BspContext ctx, RectInt leaf)
        {
            int paddingX = ctx.Random.Range(RoomPadding, RoomPadding + 2);
            int paddingY = ctx.Random.Range(RoomPadding, RoomPadding + 2);

            int width = Mathf.Max(3, leaf.width - paddingX * 2);
            int height = Mathf.Max(3, leaf.height - paddingY * 2);

            return new RoomNode
            {
                Id = ctx.NextRoomId++,
                Type = RoomType.Combat,
                GridPosition = new Vector2Int(leaf.x + paddingX, leaf.y + paddingY),
                GridSize = new Vector2Int(width, height),
                Depth = 0,
                Connections = new List<int>()
            };
        }

        private void ConnectRecursive(BspContext ctx, BspNode node)
        {
            if (node.Left == null || node.Right == null) return;

            ConnectRecursive(ctx, node.Left);
            ConnectRecursive(ctx, node.Right);

            var roomA = node.Left.GetRandomLeafRoom(ctx.Random);
            var roomB = node.Right.GetRandomLeafRoom(ctx.Random);

            if (roomA != null && roomB != null)
                ConnectRooms(ctx, roomA, roomB);
        }

        private void ConnectRooms(BspContext ctx, RoomNode a, RoomNode b)
        {
            a.Connections.Add(b.Id);
            b.Connections.Add(a.Id);
        }

        private void AssignStartRoom(BspContext ctx)
        {
            var rooms = ctx.Layout.Rooms;
            if (rooms.Count == 0) return;

            ctx.Layout.StartRoomId = rooms[0].Id;
            rooms[0].Type = RoomType.Start;

            if (rooms.Count > 1)
                rooms[^1].Type = RoomType.Boss;

            for (int i = 1; i < rooms.Count - 1; i++)
            {
                rooms[i].Type = ctx.Random.Value < LootChance
                    ? RoomType.Loot
                    : RoomType.Combat;
            }
        }

        private void AssignDepths(BspNode node, int depth)
        {
            if (node.IsLeaf)
                return;

            var leftRooms = node.Left.GetAllLeafRooms();
            for (var i = 0; i < leftRooms.Count; i++)
                leftRooms[i].Depth = depth + 1;

            var rightRooms = node.Right.GetAllLeafRooms();
            for (var i = 0; i < rightRooms.Count; i++)
                rightRooms[i].Depth = depth + 1;

            AssignDepths(node.Left, depth + 1);
            AssignDepths(node.Right, depth + 1);
        }

        private sealed class BspContext
        {
            public readonly IRandomService Random;
            public readonly DungeonLayout Layout;
            public int NextRoomId;

            public BspContext(IRandomService random, DungeonLayout layout)
            {
                Random = random;
                Layout = layout;
                NextRoomId = 0;
            }
        }
    }
}