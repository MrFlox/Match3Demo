using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText;
        IGameScore _gameScore;
        void Awake()
        {
            _gameScore = Locator.Get<IGameScore>();
            _gameScore.Score.Subscribe(OnChangeScore);
        }
        void OnChangeScore(int newValue) =>
            scoreText.text = $"Score: {_gameScore.Score.Value}";
    }
}