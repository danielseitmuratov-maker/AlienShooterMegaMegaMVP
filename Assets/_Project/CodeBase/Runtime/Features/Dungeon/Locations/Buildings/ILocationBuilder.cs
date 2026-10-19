using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Buildings
{
    public interface ILocationBuilder
    {
        UniTask<List<RoomInstance>> Build(DungeonGenerationContext context, CancellationToken token);
    }
}