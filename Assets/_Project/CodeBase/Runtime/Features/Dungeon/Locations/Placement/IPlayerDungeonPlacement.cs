using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Placement
{
    public interface IPlayerDungeonPlacement
    {
        UniTask PlaceAsync(IPlayerFacade player, RoomInstance startRoom);
    }
}


