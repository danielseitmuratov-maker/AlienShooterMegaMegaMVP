using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.Dungeon.Data;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming
{
    public interface ILocationCleaner
    {
        void CleanUp(IReadOnlyList<RoomInstance> rooms);
    }
}