using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerMovement
    {
        bool Enabled { get; set; }
        Vector3 Velocity { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        bool IsGrounded { get; }
        bool IsMoving { get; }
        float MaxSpeed { get;}
        void AddVelocity(Vector3 velocity);
        void Warp(Vector3 position);
        void FaceDirection(Vector3 direction);
    }
}