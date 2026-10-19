using _Project.CodeBase.Runtime.Features.Dungeon.Data;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Placement
{
    public sealed class PlayerDungeonPlacement : IPlayerDungeonPlacement
    {
        public UniTask PlaceAsync(IPlayerFacade player, RoomInstance startRoom)
        {
            return player.TeleportAsync(startRoom.Center);
        }
    }
}