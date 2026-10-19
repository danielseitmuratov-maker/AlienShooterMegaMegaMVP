using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Connections;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations
{
    public sealed class DungeonGenerationResult
    {
        public DungeonLayout Layout;
        public List<RoomInstance> Rooms;
        public RoomInstance StartRoom;
        public List<ConnectionInstance> Connections;
    }
}