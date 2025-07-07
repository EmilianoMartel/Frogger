using NUnit.Framework;
using Timer;

namespace Tests
{
    [TestFixture]
    public class TimerPresenterTest
    {
        private TimerModel _model;
        private MockTimerView _view;
        private TimerPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new TimerModel(10f);
            _view = new MockTimerView();
            _presenter = new TimerPresenter(_view, _model);
        }

        [Test]
        public void UpdateTime_UpdatesModelAndView()
        {
            float deltaTime = 2f;

            _presenter.UpdateTime(deltaTime);

            Assert.AreEqual(8f, _model.CurrentTime, 0.001f);
            Assert.AreEqual(8f, _view.DisplayedTime, 0.001f);
        }

        [Test]
        public void UpdateTime_WhenTimeReachesZero_InvokesOnTimeEnded()
        {
            bool eventCalled = false;

            _model = new TimerModel(1f);
            _presenter = new TimerPresenter(_view, _model);

            _presenter.OnTimeEnded += () => eventCalled = true;

            _presenter.UpdateTime(2f);

            Assert.AreEqual(0f, _model.CurrentTime, 0.001f);
            Assert.AreEqual(0f, _view.DisplayedTime, 0.001f);
            Assert.IsTrue(eventCalled);
        }
    }
}