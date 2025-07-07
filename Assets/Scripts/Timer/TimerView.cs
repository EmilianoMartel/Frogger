using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private TMP_Text _text;

        public void UpdateTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            _text.text = $"{minutes:00}:{seconds:00}";
        }
    }
}