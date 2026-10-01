using System;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Factory
{
    public interface IPlayerAsyncFactory
    {
        UniTask<IPlayerFacade> Create(Vector3 at,Quaternion rotation, Transform parent = null,Action callback = null);
    }
}