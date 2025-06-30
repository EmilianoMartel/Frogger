using NUnit.Framework;
using Timer;

namespace Tests
{
    [TestFixture]
    public class MockTimerView : TimerView
    {
        public float DisplayedTime { get; private set; }

        public override void UpdateTime(float time)
        {
            DisplayedTime = time;
        }
    }
}