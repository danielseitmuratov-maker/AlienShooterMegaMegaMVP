using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerFacade
    {
        UniTask InitializeAsync();

        Transform Transform { get; }
        Transform CameraFollowPointTransform { get; }

        UniTask TeleportAsync(Vector3 to,float delay = 0.5f);
    }
}