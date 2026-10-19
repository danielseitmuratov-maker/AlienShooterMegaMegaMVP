using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerTeleporter
    {
        UniTask TeleportAsync(Vector3 to, float delay = 0.5f);
    }
}