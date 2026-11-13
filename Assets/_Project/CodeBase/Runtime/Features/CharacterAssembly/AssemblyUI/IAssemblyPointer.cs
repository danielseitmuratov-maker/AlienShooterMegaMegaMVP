
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public interface IAssemblyPointer
    {
        Vector3 ScreenToWorld(Vector2 screenPosition);
        AssemblySlotView RaycastSlot(Vector2 screenPosition);
    }
}