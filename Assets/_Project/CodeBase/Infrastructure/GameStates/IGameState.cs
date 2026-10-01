using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.GameStates
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Exit();
    }
}