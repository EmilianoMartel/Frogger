using System;
using UnityEngine;

namespace Timer
{
    public class TimerPresenter
    {
        private ITimerView _view;
        private TimerModel _model;

        public event Action OnTimeEnded;

        public TimerPresenter(ITimerView view, TimerModel model)
        {
            _view = view;
            _model = model;
        }

        public void UpdateTime(float deltaTime)
        {
            float updatedTime = _model.UpdateTime(deltaTime);

            if (Mathf.Approximately(updatedTime, 0f) || updatedTime <= 0f)
            {
                OnTimeEnded?.Invoke();
            }

            _view.UpdateTime(_model.CurrentTime);
        }
    }
}