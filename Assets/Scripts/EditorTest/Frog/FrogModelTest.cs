using NUnit.Framework;
using UnityEngine;
using Frog;

namespace Tests
{
    [TestFixture]
    public class FrogModelTest
    {
        [Test]
        public void Constructor_SetsInitialPropertiesCorrectly()
        {
            // Arrange
            Vector2 position = new Vector2(1, 2);
            Vector2 boundTopLeft = new Vector2(-5, 5);
            Vector2 boundBottomRight = new Vector2(5, -5);

            // Act
            FrogModel frog = new FrogModel(position, boundTopLeft, boundBottomRight);

            // Assert
            Assert.AreEqual(position, frog.Position);
            Assert.AreEqual(boundTopLeft, frog.BoundTopLeft);
            Assert.AreEqual(boundBottomRight, frog.BoundBottomRight);
        }

        [Test]
        public void SetPosition_UpdatesPositionCorrectly()
        {
            // Arrange
            FrogModel frog = new FrogModel(Vector2.zero, Vector2.zero, Vector2.zero);
            Vector2 newPosition = new Vector2(3, 4);

            // Act
            frog.SetPosition(newPosition);

            // Assert
            Assert.AreEqual(newPosition, frog.Position);
        }

        [Test]
        public void Move_AddsDeltaToPosition()
        {
            // Arrange
            FrogModel frog = new FrogModel(new Vector2(1, 1), Vector2.zero, Vector2.zero);
            Vector2 delta = new Vector2(2, 3);
            Vector2 expectedPosition = frog.Position + delta;

            // Act
            frog.Move(delta);

            // Assert
            Assert.AreEqual(expectedPosition, frog.Position);
        }
    }
}
