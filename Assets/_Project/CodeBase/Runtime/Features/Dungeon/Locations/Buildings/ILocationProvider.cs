using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using _Project.CodeBase.Runtime.Features.Dungeon.Rooms;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings
{
    public interface ILocationProvider
    {
        RoomBasic GetRoomPrefab(RoomType type);
        float CellSize { get; }
        bool HasPrefabFor(RoomType type);
    }
}