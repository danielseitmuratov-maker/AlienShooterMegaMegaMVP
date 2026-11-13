using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class AssemblySlotView : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public string SlotId { get; private set; }
        [field: SerializeField] public Collider Trigger { get; private set; }
        [field: SerializeField] public Renderer HighlightRenderer { get; private set; }

        public bool IsOccupied { get; private set; }

        [SerializeField] private Color _compatibleColor;
        [SerializeField] private Color _incompatibleColor;

        private static readonly int ColorId = Shader.PropertyToID("_Color");

        private IFragmentEquipService _equipService;
        private IPlayerProvider _playerProvider;
        private IAssemblySlotRegistry _registry;
        private IFragmentDragService _dragService;
        private MaterialPropertyBlock _block;
        private CancellationTokenSource _cts;
        private SlotHighlightState _state = SlotHighlightState.None;

        [Inject]
        public void Construct(IFragmentEquipService equipService, IPlayerProvider playerProvider,
            IAssemblySlotRegistry registry, IFragmentDragService dragService)
        {
            _equipService = equipService;
            _playerProvider = playerProvider;
            _registry = registry;
            _dragService = dragService;
        }

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            _registry.Register(this);
        }
        
        
        public void SetOccupied(bool occupied) => IsOccupied = occupied;

        public void SetHighlight(SlotHighlightState state)
        {
            if (_state == state) return;
            _state = state;

            if (HighlightRenderer == null) return;

            _block ??= new MaterialPropertyBlock();
            HighlightRenderer.GetPropertyBlock(_block);
            
            _block.SetColor(ColorId, GetColor(state));
            HighlightRenderer.SetPropertyBlock(_block);
        }

        private Color GetColor(SlotHighlightState state) => state switch
        {
            SlotHighlightState.Compatible => _compatibleColor,
            SlotHighlightState.Incompatible => _incompatibleColor,
            _ => Color.white,
        };

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_dragService.IsDragging || _dragService.JustEndedDrag)
                return;
            
            if (!IsOccupied)
                return;

            DetachAsync().Forget();
        }

        private async UniTaskVoid DetachAsync()
        {
            IPlayerFacade player = _playerProvider.Current;
            
            if (player?.Character == null) 
                return;

            await _equipService.UnEquipAsync(player.Character, SlotId, _cts.Token);
        }
        
        private void OnDisable()
        {
            _registry.Unregister(this);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}