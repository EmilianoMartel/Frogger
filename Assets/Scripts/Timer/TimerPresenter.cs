using System;
using UnityEngine;

namespace Timer
{
    public class TimerPresenter
    {
        private TimerView _view;
        private TimerModel _model;

        public event Action OnTimeEnded;

        public TimerPresenter(TimerView view, TimerModel model)
        {
            _view = view;
            _model = model;
        }

        public void UpdateTime(float deltaTime)
        {
            float time = _model.UpdateTime(deltaTime);
            if (time <= 0)
            {
                time = 0;
                OnTimeEnded?.Invoke();
            }

            _view.UpdateTime(time);
        }
    }
}