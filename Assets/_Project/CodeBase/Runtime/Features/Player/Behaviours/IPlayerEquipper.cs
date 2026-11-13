using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerEquipper
    {
        UniTask<bool> EquipSelectedAsync(CancellationToken token);
        void ClearSelection();
    }
}