using _Project.CodeBase.Runtime.Features.Dungeon.Components;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Data.Connections
{
    public class ConnectionInstance
    {
        public int ConnectionId;
        public ConnectionType Type;
        public ConnectionBasic Root;
        public Vector3 Center;
        public Bounds Bounds;
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