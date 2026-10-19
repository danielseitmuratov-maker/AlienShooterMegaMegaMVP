using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Common.Factories
{
    public interface IAsyncFactory<TInstance>
    {
        UniTask<TInstance> Create(Vector3 at,Quaternion startRotation,Transform parent = null);
    }
}