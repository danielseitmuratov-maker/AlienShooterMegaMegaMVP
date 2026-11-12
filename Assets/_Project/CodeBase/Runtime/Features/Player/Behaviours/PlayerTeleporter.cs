using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerTeleporter : IPlayerTeleporter
    {
        private readonly IPlayerMovement _movement;

        public PlayerTeleporter(IPlayerMovement movement)
        {
            _movement = movement;
        }
        
        public async UniTask TeleportAsync(Vector3 to, float delay = 0.5f)
        {
            if (delay > 0) 
                await UniTask.Delay(TimeSpan.FromSeconds(delay));

            _movement.Warp(to);
        }
    }
}