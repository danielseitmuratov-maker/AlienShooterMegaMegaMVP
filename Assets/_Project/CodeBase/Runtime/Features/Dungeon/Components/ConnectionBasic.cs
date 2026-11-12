using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Runtime.Features.Dungeon.Data.Connections;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Components
{
    public class ConnectionBasic : MonoBehaviour
    {
        public ConnectionType Type => _type;
        public IReadOnlyList<EntrancePoint> Entrances => _entrances;
        
        [SerializeField] private ConnectionType _type;
        [SerializeField] private List<EntrancePoint> _entrances;
        

        private void Awake()
        {
            for (int i = 0; i < _entrances.Count; i++)
            {
                EntrancePoint entrance = _entrances[i];
                entrance.SetPlugActive(true);
            }
        }

        public void UseEntrances(IReadOnlyList<int> indices)
        {
            for (int i = 0; i < _entrances.Count; i++)
            {
                bool used = indices.Contains(i);
                _entrances[i].SetPlugActive(!used);
            }
        }
    }
}