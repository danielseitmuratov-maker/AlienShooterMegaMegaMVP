using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Connections;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon
{
    public class DungeonLayout
    {
        public int Seed;
        public List<RoomNode> Rooms;
        public List<ConnectionNode> Connections;
        public int StartRoomId;
        public int BossRoomId;
    }
}