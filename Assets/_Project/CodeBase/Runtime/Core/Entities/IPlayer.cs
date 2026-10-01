using UnityEngine;

namespace _Project.CodeBase.Runtime.Core.Entities
{
    public interface IPlayer
    {
        Transform Transform { get; }
        bool IsAlive { get; }
    }
}