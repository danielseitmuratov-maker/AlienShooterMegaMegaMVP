using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public interface IFragmentDragService
    {
        bool IsDragging { get; }
        bool JustEndedDrag { get; }
        FragmentInstance DraggedFragment { get; }

        event Action<FragmentInstance> DragStarted;
        event Action<FragmentInstance, bool> DragEnded;

        void BeginDrag(FragmentInstance fragment);
        void CancelDrag();
    }
}