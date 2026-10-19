using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Rooms
{
    public class RoomInstance
    {
        public int RoomId;
        public RoomType Type;
        public Vector3 Center;
        public Bounds Bounds;
        public RoomBasic Root;
        public bool IsActive;

        public void SetActive(bool active)
        {
            if (IsActive == active) 
                return;
            if (Root == null)
                return;

            IsActive = active;
            Root.gameObject.SetActive(active);
        }
    }
}