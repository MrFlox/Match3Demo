using System;
using Core;

namespace Infrastructure
{
    public interface ISceneLoader: IService
    {
        void LoadScene(string sceneName, Action onComplete=null);
    }
}