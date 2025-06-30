using NUnit.Framework;
using UnityEngine;
using Frog;

namespace Tests
{
    [TestFixture]
    public class FrogPresenterTest
    {
        private FrogModel _model;
        private MockFrogView _view;
        private MockInputHandler _inputHandler;
        private FrogPresenter _presenter;
        private float _moveSpeed = 5f;

        [SetUp]
        public void Setup()
        {
            _model = new FrogModel(Vector2.zero, new Vector2(-10, 10), new Vector2(10, -10));
            _view = new MockFrogView();
            _inputHandler = new MockInputHandler();
            _presenter = new FrogPresenter(_model, _view, _inputHandler, _moveSpeed);
        }

        [Test]
        public void SetSpawnPosition_UpdatesModelAndView()
        {
            Vector2 spawn = new Vector2(2, 3);
            _presenter.SetSpawnPosition(spawn);

            Assert.AreEqual(spawn, _model.Position);
            Assert.AreEqual(spawn, _view.LastPosition);
        }

        [Test]
        public void OnMove_UpdatesDirectionCorrectly()
        {
            _inputHandler.SimulateMove(new Vector2(1, 0));
            _presenter.Update();

            float expectedMovement = _moveSpeed * 0.02f;
            Vector2 expectedPosition = new Vector2(expectedMovement, 0);

            Assert.AreEqual(expectedPosition.x, _model.Position.x, 0.001f);
        }

        [Test]
        public void Movement_RespectsBoundaries()
        {
            _inputHandler.SimulateMove(new Vector2(100, 100));
            _presenter.Update();

            Assert.LessOrEqual(_model.Position.x, _model.BoundBottomRight.x);
            Assert.LessOrEqual(_model.Position.y, _model.BoundTopLeft.y);
        }

        [Test]
        public void TriggerCarTag_InvokesCarEvent()
        {
            bool carTriggered = false;
            _presenter.OnCarTriggerEntred += () => carTriggered = true;

            var car = new GameObject().AddComponent<BoxCollider2D>();
            car.tag = "Car";

            _view.SimulateTrigger(car);

            Assert.IsTrue(carTriggered);
        }

        [Test]
        public void TriggerFinalZone_InvokesFinalZoneEvent()
        {
            bool finalZoneTriggered = false;
            _presenter.OnFinalZoneEntered += () => finalZoneTriggered = true;

            var zone = new GameObject().AddComponent<BoxCollider2D>();
            zone.tag = "FinalZone";

            _view.SimulateTrigger(zone);

            Assert.IsTrue(finalZoneTriggered);
        }
    }
}