using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class AssemblyPointer : IAssemblyPointer
    {
        private readonly UnityEngine.Camera _camera;
        private readonly IAssemblySlotRegistry _slotRegistry;
        private readonly Plane _plane = new(Vector3.up, Vector3.zero);

        public AssemblyPointer(
            [Inject(Id = "AssemblyCamera")] Camera camera,
            IAssemblySlotRegistry slotRegistry)
        {
            _camera = camera;
            _slotRegistry = slotRegistry;
        }

        public Vector3 ScreenToWorld(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            
            return _plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : Vector3.zero;
        }

        public AssemblySlotView RaycastSlot(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            AssemblySlotView best = null;
            float bestDistance = float.MaxValue;

            IReadOnlyList<AssemblySlotView> slots = _slotRegistry.Slots;
            
            for (int i = 0; i < slots.Count; i++)
            {
                AssemblySlotView slot = slots[i];
                
                if (slot.Trigger == null)
                    continue;
                
                if (!slot.Trigger.Raycast(ray, out RaycastHit hit, 1000f)) 
                    continue;
                
                if (hit.distance < bestDistance)
                {
                    bestDistance = hit.distance;
                    best = slot;
                }
            }

            return best;
        }
    }
}