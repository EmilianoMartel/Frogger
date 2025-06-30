using NUnit.Framework;
using UnityEngine;
using Cars;

namespace Tests
{
    [TestFixture]
    public class TestCarModel
    {
        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            Vector2 startPosition = new Vector2(0, 0);
            Vector2 moveDirection = new Vector2(3, 4);
            float speed = 5f;
            Color color = Color.red;

            // Act
            CarModel car = new CarModel(startPosition, moveDirection, speed, color);

            // Assert
            Assert.AreEqual(startPosition, car.Position);
            Assert.AreEqual(moveDirection.normalized, car.MoveDirection);
            Assert.AreEqual(speed, car.Speed);
            Assert.AreEqual(color, car.Color);
        }

        [Test]
        public void Move_UpdatesPositionCorrectly()
        {
            // Arrange
            CarModel car = new CarModel(Vector2.zero, Vector2.right, 5f, Color.blue);
            Vector2 nextPosition = new Vector2(10, 20);

            // Act
            car.Move(nextPosition);

            // Assert
            Assert.AreEqual(nextPosition, car.Position);
        }

        [Test]
        public void MoveDirection_IsNormalized()
        {
            // Arrange
            Vector2 direction = new Vector2(5, 0);
            CarModel car = new CarModel(Vector2.zero, direction, 5f, Color.green);

            // Act & Assert
            Assert.AreEqual(Vector2.right, car.MoveDirection);
        }
    }
}

