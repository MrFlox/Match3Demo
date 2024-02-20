using Core;
using Match3;
using UnityEngine;

namespace Infrastructure
{

    public class Bootstrap: MonoBehaviour
    {
        IStateManager _stateMachine;

        void Awake()
        {



            ISceneLoader sceneLoader = new SceneLoader();
            Locator.Register(sceneLoader);
            _stateMachine = new StateMachine(sceneLoader);
            Locator.Register(_stateMachine as IStateManager);

            _stateMachine.SetState(StateMachine.States.InitServices);

            DontDestroyOnLoad(this);
        }
    }
}