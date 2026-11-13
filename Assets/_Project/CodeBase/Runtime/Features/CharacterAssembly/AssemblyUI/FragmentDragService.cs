using System;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class FragmentDragService : IFragmentDragService, ITickable, IDisposable
    {
        private const float DragCooldownSeconds = 0.15f;

        public bool IsDragging { get; private set; }
        public bool JustEndedDrag => Time.unscaledTime < _dragCooldownUntil;
        public FragmentInstance DraggedFragment { get; private set; }

        public event Action<FragmentInstance> DragStarted;
        public event Action<FragmentInstance, bool> DragEnded;

        private readonly IAssemblyPointer _pointer;
        private readonly IAssemblySlotRegistry _slotRegistry;
        private readonly IFragmentEquipService _equipService;
        private readonly IPlayerProvider _playerProvider;
        private readonly IInstantiator _instantiator;
        private readonly Transform _dragRoot;
        private readonly AssemblyDraggableView _draggablePrefab;

        private AssemblyDraggableView _draggable;
        private AssemblySlotView _currentSlot;
        private CancellationTokenSource _cts;
        private float _dragCooldownUntil;

        public FragmentDragService(
            IAssemblyPointer pointer,
            IAssemblySlotRegistry slotRegistry,
            IFragmentEquipService equipService,
            IPlayerProvider playerProvider,
            IInstantiator instantiator,
            [Inject(Id = "AssemblyDragRoot")] Transform dragRoot,
            AssemblyDraggableView draggablePrefab)
        {
            _pointer = pointer;
            _slotRegistry = slotRegistry;
            _equipService = equipService;
            _playerProvider = playerProvider;
            _instantiator = instantiator;
            _dragRoot = dragRoot;
            _draggablePrefab = draggablePrefab;
        }

        public void BeginDrag(FragmentInstance fragment)
        {
            if (IsDragging || fragment?.DefinitionConfig == null) return;

            _cts = new CancellationTokenSource();
            DraggedFragment = fragment;

            _draggable = _instantiator.InstantiatePrefabForComponent<AssemblyDraggableView>(
                _draggablePrefab, _dragRoot);
            _draggable.Initialize(fragment.DefinitionConfig);

            IsDragging = true;
            DragStarted?.Invoke(fragment);
        }

        public void Tick()
        {
            if (!IsDragging) return;
            if (Pointer.current == null) return;

            Vector2 screenPos = Pointer.current.position.ReadValue();
            UpdateDragPosition(screenPos);

            if (Pointer.current.press.wasReleasedThisFrame)
                EndDragAsync().Forget();
        }

        private void UpdateDragPosition(Vector2 screenPos)
        {
            Vector3 worldPos = _pointer.ScreenToWorld(screenPos);
            _draggable?.SetWorldPosition(worldPos);

            AssemblySlotView slot = _pointer.RaycastSlot(screenPos);
            SetCurrentSlot(slot);
        }

        private void SetCurrentSlot(AssemblySlotView newSlot)
        {
            if (newSlot == _currentSlot) return;

            if (_currentSlot != null)
                _currentSlot.SetHighlight(SlotHighlightState.None);

            _currentSlot = newSlot;

            if (_currentSlot != null)
            {
                bool compatible =
                    !_currentSlot.IsOccupied &&
                    DraggedFragment.DefinitionConfig.IsCompatibleWith(
                        SlotTypeOf(_currentSlot.SlotId));

                _currentSlot.SetHighlight(compatible
                    ? SlotHighlightState.Compatible
                    : SlotHighlightState.Incompatible);
            }
        }

        private SlotType SlotTypeOf(string slotId)
        {
            IPlayerFacade player = _playerProvider.Current;
            if (player?.Character == null) return SlotType.None;

            var slots = player.Character.Config.Slots;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].SlotId == slotId) return slots[i].SlotType;
            }
            return SlotType.None;
        }

        private async UniTaskVoid EndDragAsync()
        {
            AssemblySlotView slot = _currentSlot;
            FragmentInstance fragment = DraggedFragment;

            _currentSlot?.SetHighlight(SlotHighlightState.None);
            _currentSlot = null;

            DestroyDraggable();
            IsDragging = false;
            DraggedFragment = null;
            _dragCooldownUntil = Time.unscaledTime + DragCooldownSeconds;

            bool equipped = false;

            if (slot != null && fragment != null && !slot.IsOccupied)
            {
                IPlayerFacade player = _playerProvider.Current;
                if (player?.Character != null)
                {
                    equipped = await _equipService.EquipAsync(
                        player.Character, fragment, slot.SlotId, _cts.Token);
                }
            }

            _cts?.Dispose();
            _cts = null;

            DragEnded?.Invoke(fragment, equipped);
        }

        public void CancelDrag()
        {
            if (!IsDragging) return;

            FragmentInstance fragment = DraggedFragment;

            _currentSlot?.SetHighlight(SlotHighlightState.None);
            _currentSlot = null;

            DestroyDraggable();
            IsDragging = false;
            DraggedFragment = null;
            _dragCooldownUntil = Time.unscaledTime + DragCooldownSeconds;

            _cts?.Dispose();
            _cts = null;

            DragEnded?.Invoke(fragment, false);
        }

        private void DestroyDraggable()
        {
            if (_draggable != null)
                UnityEngine.Object.Destroy(_draggable.gameObject);
            _draggable = null;
        }

        public void Dispose()
        {
            CancelDrag();
        }
    }
}