using System;
using System.Threading;
using _Project.CodeBase.Infrastructure.Services.Input;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class PlayerFragmentsEquipper : IInitializable, IDisposable
    {
        private readonly IPlayerEquipper _equipper;
        private readonly IInputService _inputService;

        private CancellationTokenSource _cts;
        private bool _isEquipping;

        public PlayerFragmentsEquipper(IPlayerEquipper equipper, IInputService inputService)
        {
            _equipper = equipper;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            _inputService.EquipFragmentPerformed += OnEquipTriggered;
        }

        public void Dispose()
        {
            _inputService.EquipFragmentPerformed -= OnEquipTriggered;
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private void OnEquipTriggered() => 
            EquipAsync().Forget();

        private async UniTaskVoid EquipAsync()
        {
            if (_isEquipping)
                return;

            _isEquipping = true;
            
            try
            {
                await _equipper.EquipSelectedAsync(_cts.Token);
            }
            catch (OperationCanceledException) { }
            finally
            {
                _isEquipping = false;
            }
        }
    }
}