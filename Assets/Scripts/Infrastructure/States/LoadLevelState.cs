using Core;
using UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IGameState
    {
        const string SceneName = "Level";
        readonly ISceneLoader _sceneLoader;
        public LoadLevelState() => _sceneLoader = Locator.Get<ISceneLoader>();

        public void EnterState() => _sceneLoader.LoadScene(SceneName,
            () => Locator.Get<IGameTimerService>().StartRound());
    }
}