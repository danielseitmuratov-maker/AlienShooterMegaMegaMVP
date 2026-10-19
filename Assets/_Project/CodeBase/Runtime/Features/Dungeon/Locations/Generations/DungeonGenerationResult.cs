using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations
{
    public sealed class DungeonGenerationResult
    {
        public DungeonLayout Layout;
        public List<RoomInstance> Rooms;
        public RoomInstance StartRoom;
    }
}