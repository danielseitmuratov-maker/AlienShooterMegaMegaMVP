using Zenject;

namespace _Project.CodeBase.Infrastructure.GameStates
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly IInstantiator _instantiator;

        public GameStateFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TState Create<TState>() where TState : IGameState => 
            _instantiator.Instantiate<TState>();
    }
}