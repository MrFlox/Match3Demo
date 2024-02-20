using System.Collections;
using Core;
using Infrastructure;
using ScriptableObjects;
using TriInspector;
using UI;
using UnityEngine;

public class GameTimer : MonoBehaviour, IGameTimerService
{
   public float ElapsedTime = -1f;
    GameSettings _settings;
    void Awake()
    {
        _settings = Locator.Get<GameSettings>();
        Locator.Register(this as IGameTimerService);
    }

    [Button]
    public void StartRound()
    {
        ElapsedTime = _settings.MatchRoundTime;
        StartCoroutine(Round());
    }

    IEnumerator Round()
    {
        var startTime = Time.time;
        while (Time.time - startTime < _settings.MatchRoundTime)
        {
            ElapsedTime = _settings.MatchRoundTime - (Time.time - startTime);
            yield return null;
        }
        ElapsedTime = 0;
        Locator.Get<IStateManager>().SetState(StateMachine.States.GameOver);
    }
    public string GetElapcedTime()
    {
        if (ElapsedTime == -1)
            return "Not Started";
        return $"Time: {Mathf.RoundToInt(ElapsedTime)}s";
    }
}