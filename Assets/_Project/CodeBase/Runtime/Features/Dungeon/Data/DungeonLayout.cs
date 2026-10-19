using System.Collections.Generic;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data
{
    public class DungeonLayout
    {
        public int Seed;
        public List<RoomNode> Rooms;
        public List<Connection> Connections;
        public int StartRoomId;
    }
}