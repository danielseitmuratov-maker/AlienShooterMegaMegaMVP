using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Scenes
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string name,Action onLoaded);
    }
}