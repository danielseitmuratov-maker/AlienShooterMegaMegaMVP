using _Project.CodeBase.Runtime.Features.Dungeon.Data.Dungeon;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations.Layout
{
    public interface IDungeonLayoutStrategy
    {
        LayoutAlgorithmType Type { get; }

        DungeonLayout Create(DungeonGenerationContext context);
    }
}