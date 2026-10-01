using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Camera
{
    public interface ICameraProvider
    {
        Quaternion Rotation { get; }
    }
}