using System.Threading;
using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings.Rooms
{
    public interface IRoomAssembler
    {
        UniTask<RoomBasic> AssembleAsync(RoomNode node, Transform parent, CancellationToken token);
    }
}