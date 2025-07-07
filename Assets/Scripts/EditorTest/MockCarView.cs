using System;
using UnityEngine;
using Cars;

namespace Tests
{
    public class MockCarView : ICarView
    {
        public Color CurrentColor { get; private set; }
        public Vector2 LastUpdatedPosition { get; private set; }
        public bool IsEnabled { get; private set; }
        public bool IsSubscribed { get; private set; }

        private Action<Collider2D> _onTrigger;

        public event Action<Collider2D> OnObjectTriggerEnter
        {
            add
            {
                IsSubscribed = true;
                _onTrigger += value;
            }
            remove
            {
                IsSubscribed = false;
                _onTrigger -= value;
            }
        }

        public void SetNewColor(Color color)
        {
            CurrentColor = color;
        }

        public void UpdatePosition(Vector2 position)
        {
            LastUpdatedPosition = position;
        }

        public void EnableView()
        {
            IsEnabled = true;
        }

        public void DisableView()
        {
            IsEnabled = false;
        }

        public void SimulateTrigger(Collider2D collider)
        {
            _onTrigger?.Invoke(collider);
        }
    }
}