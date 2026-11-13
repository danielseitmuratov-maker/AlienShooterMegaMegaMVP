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
        event Action EquipFragmentPerformed;
        event Action EquipFragmentCancelled;

        event Action ClickPerformed;
        Vector3 PointerPosition { get;}


        Vector2 ReadMovement();
        Vector2 ReadZoom();
        Vector2 ReadCameraRotation();
    }
}