using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Events;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class FragmentIconListView : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private FragmentIconView _iconPrefab;

        private readonly Dictionary<FragmentInstance, FragmentIconView> _icons =
            new Dictionary<FragmentInstance, FragmentIconView>();

        private IFragmentDragService _dragService;
        private ICharacterAssemblyEvents _events;

        [Inject]
        public void Construct(IFragmentDragService dragService, ICharacterAssemblyEvents events)
        {
            _dragService = dragService;
            _events = events;
        }

        private void OnEnable()
        {
            _dragService.DragStarted += OnDragStarted;
            _dragService.DragEnded += OnDragEnded;
            _events.FragmentUnequipped += OnFragmentUnequipped;
        }

        private void OnDisable()
        {
            _dragService.DragStarted -= OnDragStarted;
            _dragService.DragEnded -= OnDragEnded;
            _events.FragmentUnequipped -= OnFragmentUnequipped;
        }

        public void Populate(IReadOnlyList<FragmentInstance> fragments)
        {
            Clear();
            for (int i = 0; i < fragments.Count; i++)
                AddIcon(fragments[i]);
        }

        public void Clear()
        {
            foreach (var view in _icons.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            _icons.Clear();
        }

        private void AddIcon(FragmentInstance fragment)
        {
            if (fragment == null || _icons.ContainsKey(fragment)) return;

            FragmentIconView view = Instantiate(_iconPrefab, _container);
            view.Setup(fragment);
            view.Clicked += OnIconClicked;
            _icons[fragment] = view;
        }

        private void RemoveIcon(FragmentInstance fragment)
        {
            if (fragment == null || !_icons.TryGetValue(fragment, out var view)) return;

            view.Clicked -= OnIconClicked;
            if (view != null) Destroy(view.gameObject);
            _icons.Remove(fragment);
        }

        private void OnIconClicked(FragmentInstance fragment)
        {
            _dragService.BeginDrag(fragment);
        }

        private void OnDragStarted(FragmentInstance fragment)
        {
            if (_icons.TryGetValue(fragment, out var view))
                view.SetVisible(false);
        }

        private void OnDragEnded(FragmentInstance fragment, bool equipped)
        {
            if (fragment == null) return;

            if (equipped) RemoveIcon(fragment);
            else if (_icons.TryGetValue(fragment, out var view)) view.SetVisible(true);
        }

        private void OnFragmentUnequipped(FragmentUnequippedArgs args)
        {
            AddIcon(args.Fragment);
        }
    }
}