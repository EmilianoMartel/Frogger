using System;
using UnityEngine;
using Cars;

namespace Tests
{
    public class MockCarView : CarView
    {
        public Color CurrentColor { get; private set; }
        public Vector2 LastUpdatedPosition { get; private set; }
        public bool IsSubscribed { get; private set; }
        public bool IsEnabled { get; private set; }

        public override event Action<Collider2D> OnObjectTriggerEnter;

        public override void SetNewColor(Color color)
        {
            CurrentColor = color;
        }

        public override void UpdatePosition(Vector2 position)
        {
            LastUpdatedPosition = position;
        }

        public override void EnableView()
        {
            IsEnabled = true;
        }

        public override void DisableView()
        {
            IsEnabled = false;
        }

        public void SimulateTrigger(Collider2D collider)
        {
            OnObjectTriggerEnter?.Invoke(collider);
        }

        private void OnEnable()
        {
            IsSubscribed = true;
        }

        private void OnDisable()
        {
            IsSubscribed = false;
        }
    }
}