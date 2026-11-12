using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera.Behaviours
{
    [CreateAssetMenu(fileName = "CameraFollowConfig", menuName = "SO/" + nameof(CameraFollowConfig), order = 0)]
    public class CameraFollowConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public float SmoothTime { get; private set; }
    }
}