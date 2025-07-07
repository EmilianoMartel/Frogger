using System;
using UnityEngine;
using Frog;

namespace Tests
{
    public class MockFrogView : IFrogView
    {
        public event Action<Collider2D> OnTriggerEnterEvent;
        public Vector2 LastPosition { get; private set; }

        public void UpdatePosition(Vector2 position)
        {
            LastPosition = position;
        }

        public void SimulateTrigger(Collider2D collider)
        {
            OnTriggerEnterEvent?.Invoke(collider);
        }
    }
}