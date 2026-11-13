using System;
using _Project.CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources.Implementations
{
    public sealed class WorldSlotSelectionSource : ISlotSelectionSource
    {
        public event Action<string> Selected;

        private const float MaxDistance = 100f;

        private readonly IInputService _inputService;
        private readonly UnityEngine.Camera _camera;
        private readonly LayerMask _bodyLayer;

        public WorldSlotSelectionSource(
            IInputService inputService,
            UnityEngine.Camera camera,
            LayerMask bodyLayer)
        {
            _inputService = inputService;
            _camera = camera;
            _bodyLayer = bodyLayer;
        }

        public void Enable() => _inputService.ClickPerformed += OnClick;
        public void Disable() => _inputService.ClickPerformed -= OnClick;

        private void OnClick()
        {
            Ray ray = _camera.ScreenPointToRay(_inputService.PointerPosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _bodyLayer))
                return;

            if (!hit.collider.TryGetComponent(out BodyPartTag tag))
                return;

            Selected?.Invoke(tag.SlotId);
        }
    }
}