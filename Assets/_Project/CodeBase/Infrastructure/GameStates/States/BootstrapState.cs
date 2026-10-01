using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure.GameStates.States
{
    public class BootstrapState : IGameState
    {
        private readonly SignalBus _signalBus;

        public BootstrapState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public async UniTask Enter()
        {
            await UniTask.Delay(100);
        }
        

        public async UniTask Exit()
        {
            
        }
    }
}