using _Project.CodeBase.Infrastructure.GameStates;
using _Project.CodeBase.Infrastructure.GameStates.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Levels
{
    public class LevelController : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LevelController(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<GameLoopState>().Forget();
        }
    }
}