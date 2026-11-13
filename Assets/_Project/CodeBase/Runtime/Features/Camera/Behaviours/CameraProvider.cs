using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera.Behaviours
{
    public class CameraProvider : ICameraProvider
    {
        public Transform Transform => 
            _camera != null ? _camera.transform : UnityEngine.Camera.current.transform;
        public Quaternion Rotation => 
            _camera != null ? _camera.transform.rotation : UnityEngine.Camera.current.transform.rotation;
        
        public UnityEngine.Camera Current =>
            _camera != null ? _camera : UnityEngine.Camera.current;

        private readonly UnityEngine.Camera _camera;

        public CameraProvider(UnityEngine.Camera camera)
        {
            _camera = camera;
        }
    }
}