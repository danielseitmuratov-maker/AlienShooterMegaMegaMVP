using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.GameStates
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        public event Action<IGameState> TransitionStarted;
        public event Action<IGameState> TransitionFinished;

        public IGameState Current { get; private set; }

        private readonly DiContainer _container;
        private bool _isTransitioning;

        public GameStateMachine(DiContainer container)
        {
            _container = container;
        }

        public async UniTask Enter<TState>(float delay = 0f) where TState : IGameState
        {
            if (_isTransitioning)
            {
                Debug.LogWarning($"Transition to {typeof(TState).Name} ignored — another transition in progress");
                return;
            }

            _isTransitioning = true;
            
            try
            {
                TState next = _container.Resolve<TState>();
                Debug.Log(next.GetType().Name);

                TransitionStarted?.Invoke(next);

                await TransitTo(next: next,delay: delay);

                TransitionFinished?.Invoke(next);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to transition to {typeof(TState).Name}: {e}");
                throw;
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        private async Task TransitTo<TState>( TState next,float delay) where TState : IGameState
        {
            if (delay > 0f)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));

            if (Current != null)
                await Current.Exit();

            Current = next;
            await Current.Enter();
        }
    }
}