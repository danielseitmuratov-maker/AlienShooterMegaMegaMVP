using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources
{
    public sealed class SelectionSourcesCoordinator : IInitializable, IDisposable
    {
        private readonly IFragmentSelectionService _selection;
        private readonly List<IFragmentSelectionSource> _fragmentSources;
        private readonly List<ISlotSelectionSource> _slotSources;

        public SelectionSourcesCoordinator(IFragmentSelectionService selection,
            List<IFragmentSelectionSource> fragmentSources, List<ISlotSelectionSource> slotSources)
        {
            _selection = selection;
            _fragmentSources = fragmentSources;
            _slotSources = slotSources;
        }

        public void Initialize()
        {
            for (var i = 0; i < _fragmentSources.Count; i++)
            {
                IFragmentSelectionSource source = _fragmentSources[i];
                source.Selected += _selection.SelectFragment;
                source.Enable();
            }

            for (var i = 0; i < _slotSources.Count; i++)
            {
                ISlotSelectionSource source = _slotSources[i];
                source.Selected += _selection.SelectSlot;
                source.Enable();
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _fragmentSources.Count; i++)
            {
                IFragmentSelectionSource source = _fragmentSources[i];
                source.Selected -= _selection.SelectFragment;
                source.Disable();
            }

            for (var i = 0; i < _slotSources.Count; i++)
            {
                ISlotSelectionSource source = _slotSources[i];
                source.Selected -= _selection.SelectSlot;
                source.Disable();
            }
        }
    }
}