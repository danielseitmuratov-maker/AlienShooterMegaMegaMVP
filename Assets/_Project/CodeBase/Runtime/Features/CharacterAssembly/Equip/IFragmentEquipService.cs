using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip
{
    public interface IFragmentEquipService
    {
        UniTask<bool> EquipAsync(CharacterInstance character, FragmentInstance fragment, string slotId,
            CancellationToken token);

        UniTask<FragmentInstance> UnEquipAsync(CharacterInstance character, string slotId, CancellationToken token);
    }
}