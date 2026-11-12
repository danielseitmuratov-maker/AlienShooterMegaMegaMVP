using System;
using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly
{
    public interface ICharacterDisassembler
    {
        event Action OnOperationStarted;
        event Action<CharacterInstance> OnOperationDone;
        event Action<CharacterInstance> OnOperationFailed;
        
        UniTask<IReadOnlyList<FragmentInstance>> DisAssembleAsync(CharacterInstance characterInstance,
            CancellationToken tokenSource);
    }
}