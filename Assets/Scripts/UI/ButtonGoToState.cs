using Core;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonGoToState : MonoBehaviour
    {
        [SerializeField] StateMachine.States NextState;
        [SerializeField] Button ButtonPlay;
        void Awake() =>
            ButtonPlay.onClick.AddListener(OnPlayHandler);
        void OnPlayHandler() => Locator.Get<IStateManager>().SetState(NextState);
    }
}