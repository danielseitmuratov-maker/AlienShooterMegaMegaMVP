using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Components
{
    public class EntrancePoint : MonoBehaviour
    {
        public Vector3 OutDirection => transform.TransformDirection(_outDirection).normalized;
        public Vector3 WorldPosition => transform.position;
        
        [Tooltip("Куда смотрит вход (наружу). В локальных координатах префаба.")]
        [SerializeField] private Vector3 _outDirection = Vector3.forward;

        [Tooltip("Объект-заглушка. Активируется, если вход не используется.")]
        [SerializeField] private GameObject _plug;
        

        public void SetPlugActive(bool active)
        {
            if (_plug != null)
                _plug.SetActive(active);
        }
    }
}