using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public interface IAssemblySpaceController
    {
        UniTask EnterAsync(IReadOnlyList<FragmentInstance> initialFragments);
        UniTask ExitAsync();
    }
}