using UnityEngine;
using System;

namespace Frog
{
    public class FrogView : MonoBehaviour, IFrogView

    {
        public event Action<Collider2D> OnTriggerEnterEvent;

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnterEvent?.Invoke(collision);  
        }
    }
}