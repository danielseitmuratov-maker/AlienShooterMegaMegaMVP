using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.GameStates
{
    public interface IGameStateMachine
    {
        event Action<IGameState> TransitionStarted;
        event Action<IGameState> TransitionFinished;
        IGameState Current { get; }
        UniTask Enter<TState>(float delay = 0f) where TState : IGameState;
    }
}