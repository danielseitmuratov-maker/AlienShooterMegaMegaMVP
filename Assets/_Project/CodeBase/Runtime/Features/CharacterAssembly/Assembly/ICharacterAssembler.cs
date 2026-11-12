using System;
using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly
{
    public interface ICharacterAssembler
    {
        event Action OnAssembleOperationStarted;
        event Action<CharacterInstance> OnAssembleOperationDone;
        event Action<CharacterInstance> OnAssembleOperationFailed;

        UniTask<CharacterInstance> AssembleAsync(AssemblyCharacterConfig assemblyConfig, Transform skeletonRoot,
            IReadOnlyList<FragmentInstance> fragments, CancellationToken token);

        UniTask<bool> AttachFragmentAsync(CharacterInstance character, FragmentInstance fragment, string slotId,
            CancellationToken token);

        UniTask<FragmentInstance> DetachFragmentAsync(CharacterInstance character, string slotId,
            CancellationToken token);
    }
}