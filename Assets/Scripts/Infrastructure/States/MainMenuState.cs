using Core;

namespace Infrastructure.States
{
    public class MainMenuState : IGameState
    {
        readonly ISceneLoader _sceneLoader;
        public MainMenuState() => _sceneLoader = Locator.Get<ISceneLoader>();

        public void EnterState()
        {
            _sceneLoader.LoadScene("MainMenu");
        }
    }
}