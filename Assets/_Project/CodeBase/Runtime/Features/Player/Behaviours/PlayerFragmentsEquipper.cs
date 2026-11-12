using System;
using System.Threading;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class PlayerFragmentsEquipper : IInitializable, IDisposable
    {
        private readonly IFragmentEquipService _fragmentEquipService;
        private readonly IFragmentSelectionService _selection;
        private readonly IInputService _inputService;
        private readonly IPlayerProvider _playerProvider;

        private CancellationTokenSource _cts;

        public PlayerFragmentsEquipper(IFragmentEquipService fragmentEquipService, IFragmentSelectionService selection,
            IInputService inputService, IPlayerProvider playerProvider)
        {
            _fragmentEquipService = fragmentEquipService;
            _selection = selection;
            _inputService = inputService;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            SubscribeToEvents();
        }
        

        private void SubscribeToEvents() =>
            _inputService.EquipFragmentPerformed += OnEquipFragmentPerformed;

        private void UnsubscribeFromEvents() =>
            _inputService.EquipFragmentPerformed -= OnEquipFragmentPerformed;

        private void OnEquipFragmentPerformed() =>
            EquipSelectedAsync().Forget();

        private async UniTask EquipSelectedAsync()
        {
            if (!_selection.HasSelection)
            {
                Debug.LogWarning("[Equipper] No fragment or slot selected.");
                return;
            }

            IPlayerFacade player = _playerProvider.Current;
            if (player?.Character == null)
            {
                Debug.LogWarning("[Equipper] No player or character.");
                return;
            }

            bool success = await _fragmentEquipService.EquipAsync(
                player.Character,
                _selection.SelectedFragment,
                _selection.TargetSlotId,
                _cts.Token);

            if (success)
                _selection.Clear();
        }
        
        public void Dispose()
        {
            UnsubscribeFromEvents();
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}