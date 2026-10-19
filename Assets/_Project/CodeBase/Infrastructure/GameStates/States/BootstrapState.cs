using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.GameStates.States
{
    public class BootstrapState : IGameState
    {

        public BootstrapState()
        {
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