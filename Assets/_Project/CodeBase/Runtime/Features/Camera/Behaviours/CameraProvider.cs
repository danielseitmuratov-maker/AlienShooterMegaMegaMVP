using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera.Behaviours
{
    public class CameraProvider : ICameraProvider
    {
        public Transform Transform => _transform;
        public Quaternion Rotation => _transform.rotation;

        private Transform _transform;

        public CameraProvider(Transform transform)
        {
            _transform = transform;
        }
    }
}

