using NUnit.Framework;
using Timer;

namespace Tests
{
    [TestFixture]
    public class TimerModelTest
    {
        [Test]
        public void Constructor_SetsMaxTimeAndCurrentTime()
        {
            // Arrange
            float maxTime = 10f;

            // Act
            TimerModel timer = new TimerModel(maxTime);

            // Assert
            Assert.AreEqual(maxTime, timer.MaxTime);
            Assert.AreEqual(maxTime, timer.CurrentTime);
        }

        [Test]
        public void UpdateTime_DecreasesCurrentTime()
        {
            // Arrange
            TimerModel timer = new TimerModel(10f);
            float deltaTime = 1.5f;

            // Act
            float result = timer.UpdateTime(deltaTime);

            // Assert
            Assert.AreEqual(10f - deltaTime, result);
            Assert.AreEqual(10f - deltaTime, timer.CurrentTime);
        }

        [Test]
        public void UpdateTime_AllowsNegativeTime()
        {
            // Arrange
            TimerModel timer = new TimerModel(2f);

            // Act
            timer.UpdateTime(3f);

            // Assert
            Assert.Less(timer.CurrentTime, 0f);  // Confirm negative value is allowed
        }
    }
}
