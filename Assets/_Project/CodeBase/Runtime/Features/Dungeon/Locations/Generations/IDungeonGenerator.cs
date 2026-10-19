using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations
{
    public interface IDungeonGenerator
    {
        UniTask<DungeonGenerationResult> GenerateAsync(CancellationToken token);
    }
}