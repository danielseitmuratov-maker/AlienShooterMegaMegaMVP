using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera
{
    public class CameraProvider : ICameraProvider
    {
        public Quaternion Rotation => _transform.rotation;

        private readonly Transform _transform;

        public CameraProvider(Transform transform)
        {
            _transform = transform;
        }
    }
}