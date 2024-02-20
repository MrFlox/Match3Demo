using Core;
using UI;

namespace Infrastructure.States
{
    public class GameOverState:IGameState
    {
        readonly ISceneLoader _sceneLoader;
        public GameOverState() => _sceneLoader = Locator.Get<ISceneLoader>();

        public void EnterState()
        {
            _sceneLoader.LoadScene("GameOver");
            Locator.Unregister<IGameTimerService>();
        }
    }
}