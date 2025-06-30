using NUnit.Framework;
using UnityEngine;
using Cars;

namespace Tests
{
    [TestFixture]
    public class TestCarPresenter
    {
        private CarModel _model;
        private MockCarView _view;
        private CarPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new CarModel(Vector2.zero, Vector2.right, 5f, Color.green);
            _view = new MockCarView();
            _presenter = new CarPresenter(_model, _view);
        }

        [Test]
        public void Constructor_InitializesViewWithColorAndPosition()
        {
            // Assert
            Assert.AreEqual(_model.Color, _view.CurrentColor);
            Assert.AreEqual(_model.Position, _view.LastUpdatedPosition);
            Assert.IsTrue(_view.IsSubscribed);
        }

        [Test]
        public void Move_UpdatesModelAndViewPosition()
        {
            // Arrange
            float deltaTime = 0.02f;

            Vector2 expectedMovement = _model.MoveDirection * (_model.Speed * deltaTime);
            Vector2 expectedPosition = _model.Position + expectedMovement;

            // Act
            _presenter.Move();

            // Assert
            Assert.AreEqual(expectedPosition, _model.Position);
            Assert.AreEqual(expectedPosition, _view.LastUpdatedPosition);
        }

        [Test]
        public void Respawn_UpdatesPositionAndEnablesView()
        {
            // Arrange
            GameObject go = new GameObject();
            go.transform.position = new Vector2(10, 20);

            // Act
            _presenter.Respawn(go.transform);

            // Assert
            Assert.AreEqual(go.transform.position, _model.Position);
            Assert.AreEqual(go.transform.position, _view.LastUpdatedPosition);
            Assert.IsTrue(_view.IsEnabled);
        }

        [Test]
        public void Dispose_UnsubscribesEvent()
        {
            // Act
            _presenter.Dispose();

            // Assert
            Assert.IsFalse(_view.IsSubscribed);
        }

        [Test]
        public void TriggeringWithPlayerTag_DisablesViewAndRequestsReturnToPool()
        {
            // Arrange
            bool eventCalled = false;
            _presenter.OnRequestReturnToPool += (_) => eventCalled = true;

            var collider = new GameObject().AddComponent<BoxCollider2D>();
            collider.tag = "Player";

            // Act
            _view.SimulateTrigger(collider);

            // Assert
            Assert.IsTrue(eventCalled);
            Assert.IsFalse(_view.IsEnabled);
        }

        [Test]
        public void TriggeringWithEndZoneTag_DisablesViewAndRequestsReturnToPool()
        {
            // Arrange
            bool eventCalled = false;
            _presenter.OnRequestReturnToPool += (_) => eventCalled = true;

            var collider = new GameObject().AddComponent<BoxCollider2D>();
            collider.tag = "EndZone";

            // Act
            _view.SimulateTrigger(collider);

            // Assert
            Assert.IsTrue(eventCalled);
            Assert.IsFalse(_view.IsEnabled);
        }
    }
}
