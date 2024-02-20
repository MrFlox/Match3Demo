using TMPro;
using UnityEngine;

namespace UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        [SerializeField] GameTimer _timer;
        void Update()
        {
            text.text = _timer.GetElapcedTime();
        }
    }
}