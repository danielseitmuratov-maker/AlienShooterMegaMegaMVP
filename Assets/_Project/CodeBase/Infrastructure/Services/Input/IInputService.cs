using System;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action AttackPerformed;
        event Action JumpPerformed;
        event Action JumpCancelled;
        event Action CrouchPerformed;
        event Action CrouchCanceled;
        Vector2 ReadMovement();
    }
}