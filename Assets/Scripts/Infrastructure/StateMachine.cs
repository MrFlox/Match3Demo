using System;
using System.Collections.Generic;
using Infrastructure.States;
using TriInspector;

namespace Infrastructure
{
    public class StateMachine : IStateManager
    {
        public event Action<States> OnChangeState;
        Dictionary<States, IGameState> _states;
        States _currentState = States.None;
        public enum States
        {
            None,
            MainMenu,
            LoadLevel,
            InitServices,
            GameOver
        }
        public StateMachine(ISceneLoader sceneLoader) => InitStates();
        void InitStates()
        {
            _states = new Dictionary<States, IGameState>()
            {
                [States.MainMenu] = new MainMenuState(),
                [States.LoadLevel] = new LoadLevelState(),
                [States.InitServices] = new InitServicesState(this),
                [States.GameOver] = new GameOverState()
            };
        }

        [Button]
        public void SetState(States newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;
            OnChangeState?.Invoke(_currentState);
            _states[newState].EnterState();
        }
    }
}