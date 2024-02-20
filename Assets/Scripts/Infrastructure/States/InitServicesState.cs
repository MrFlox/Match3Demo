using Core;
using ScriptableObjects;
using UnityEngine;

namespace Infrastructure.States
{
    public class InitServicesState : IGameState
    {
        readonly StateMachine _stateMachine;
        public InitServicesState(StateMachine stateMachine) => _stateMachine = stateMachine;
        public void EnterState()
        {
            Locator.Register(new GameScoreModel() as IGameScore);
            GameSettings myScriptableObject = Resources.Load<GameSettings>("Data/GameSettings");
            Locator.Register(myScriptableObject);
            _stateMachine.SetState(StateMachine.States.MainMenu);
        }
    }
}