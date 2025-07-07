using NUnit.Framework;
using Timer;

namespace Tests
{
    [TestFixture]
    public class MockTimerView : ITimerView
    {
        public float DisplayedTime { get; private set; }

        public void UpdateTime(float time)
        {
            DisplayedTime = time;
        }
    }
}