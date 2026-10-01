using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Infrastructure.Services.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string nextScene, Action onLoaded)
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (activeScene.name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(nextScene);
            await UniTask.WaitUntil(() => waitNextScene.IsDone);
            onLoaded?.Invoke();
        }
    }
}