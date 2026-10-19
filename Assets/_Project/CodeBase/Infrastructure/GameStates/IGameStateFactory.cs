namespace _Project.CodeBase.Infrastructure.GameStates
{
    public interface IGameStateFactory
    {
        TState Create<TState>() where TState : IGameState;
    }
}