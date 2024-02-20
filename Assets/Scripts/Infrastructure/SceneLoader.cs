using System;
using Core;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    class SceneLoader : ISceneLoader, IService
    {
        static void LoadWithSceneManager(string sceneName, Action onComplete)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += (_) => onComplete?.Invoke();
        }
        public void LoadScene(string sceneName, Action onComplete) =>
            LoadWithSceneManager(sceneName, onComplete);
    }
}