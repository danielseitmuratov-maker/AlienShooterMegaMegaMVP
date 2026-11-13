using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class PlayerEquipper : IPlayerEquipper
    {
        private readonly IFragmentEquipService _equipService;
        private readonly IFragmentSelectionService _selection;
        private readonly IPlayerProvider _playerProvider;

        public PlayerEquipper(IFragmentEquipService equipService, IFragmentSelectionService selection,
            IPlayerProvider playerProvider)
        {
            _equipService = equipService;
            _selection = selection;
            _playerProvider = playerProvider;
        }

        public async UniTask<bool> EquipSelectedAsync(CancellationToken token)
        {
            if (!_selection.HasSelection)
            {
                Debug.LogWarning("[PlayerEquipper] No fragment or slot selected.");
                return false;
            }

            IPlayerFacade player = _playerProvider.Current;
            
            if (player?.Character == null)
            {
                Debug.LogWarning("[PlayerEquipper] No player or character.");
                return false;
            }

            bool success = await _equipService.EquipAsync(
                player.Character,
                _selection.SelectedFragment,
                _selection.TargetSlotId,
                token);

            if (success)
                _selection.Clear();

            return success;
        }

        public void ClearSelection() => 
            _selection.Clear();
    }
}