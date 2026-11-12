using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerSpawner
    {
        UniTask<IPlayerFacade> SpawnAsync(AssemblyCharacterConfig config, IReadOnlyList<FragmentInstance> fragments,
            Vector3 position, Quaternion rotation, CancellationToken token);

        UniTask DespawnAsync(IPlayerFacade player, CancellationToken token);
    }
}