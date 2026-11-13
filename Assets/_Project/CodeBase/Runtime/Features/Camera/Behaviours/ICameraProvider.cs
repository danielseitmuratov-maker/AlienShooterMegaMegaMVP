using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera.Behaviours
{
    public interface ICameraProvider
    {
        Transform Transform { get; }
        Quaternion Rotation { get; }
        UnityEngine.Camera Current { get; }
    }
}