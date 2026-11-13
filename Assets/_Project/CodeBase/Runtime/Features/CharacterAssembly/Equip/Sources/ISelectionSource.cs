using System;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources
{
    public interface ISelectionSource<out T>
    {
        event Action<T> Selected;

        void Enable();
        void Disable();
    }
}

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Equip.Sources
{
}