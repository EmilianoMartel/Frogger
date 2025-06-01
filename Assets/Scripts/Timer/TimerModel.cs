using UnityEngine;

namespace Timer
{
    public class TimerModel
    {
        public float MaxTime { get; private set; }
        public float CurrentTime { get; private set; }

        public TimerModel(float maxTime)
        {
            MaxTime = maxTime;
            CurrentTime = maxTime;
        }

        public float UpdateTime(float deltaTime)
        {
            CurrentTime -= deltaTime;

            return CurrentTime;
        }
    }
}