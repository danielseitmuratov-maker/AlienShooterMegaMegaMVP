using _Project.CodeBase.Infrastructure.GameStates;
using _Project.CodeBase.Infrastructure.GameStates.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class ApplicationController : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ApplicationController(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Initialize()
        {
            //пока что оставлю так до момента пока не напишу тот необходимый мимнимум что мне требуется 
            _gameStateMachine.Enter<GameLoopState>().Forget();
        }
    }
}