using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerFacade
    {
        Transform Transform { get; }
        Transform CameraFollowPointTransform { get; }
        CharacterInstance Character { get; }

        void SetCharacter(CharacterInstance character);
        UniTask InitializeAsync();
        UniTask TeleportAsync(Vector3 to, float delay = 0.5f);
    }
}