using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerMovement
    {
        public event Action<float> MoveSpeedChanged;
        public event Action Jumped;
        
        bool Enabled { get; }
        Vector3 Velocity { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        bool IsGrounded { get; }
        bool IsMoving { get; }
        float MaxSpeed { get;}

        public void AddVelocity(Vector3 velocity);

        public void Warp(Vector3 position);
        public void FaceDirection(Vector3 direction);
    }
}