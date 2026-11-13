using System;
using _Project.CodeBase.Infrastructure.Services.Input;
using _Project.CodeBase.Runtime.Features.Camera.Behaviours;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources.Implementations
{
    public sealed class WorldFragmentSelectionSource : IFragmentSelectionSource
    {
        public event Action<FragmentInstance> Selected;

        private const float MaxDistance = 100f;

        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly LayerMask _fragmentLayer;

        public WorldFragmentSelectionSource(IInputService inputService, ICameraProvider cameraProvider,
            LayerMask fragmentLayer)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _fragmentLayer = fragmentLayer;
        }

        public void Enable() =>
            _inputService.ClickPerformed += OnClick;
        

        private void OnClick()
        {
            Ray ray = _cameraProvider.Current.ScreenPointToRay(_inputService.PointerPosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _fragmentLayer))
                return;

            if (!hit.collider.TryGetComponent(out PartDropInstance drop))
                return;

            Selected?.Invoke(drop.Fragment);
        }
        
        public void Disable() =>
            _inputService.ClickPerformed -= OnClick;
    }
}