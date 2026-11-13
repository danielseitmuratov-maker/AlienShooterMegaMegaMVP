using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IStartingFragmentsProvider
    {
        IReadOnlyList<FragmentInstance> GetStartingFragments();
    }
}