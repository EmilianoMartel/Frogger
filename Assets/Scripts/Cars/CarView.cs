using System;
using UnityEngine;

namespace Cars
{
    public class CarView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public event Action<Collider2D> OnObjectTriggerEnter;

        public void SetNewColor(Color color)
        {
            if(_spriteRenderer == null && transform.TryGetComponent(out SpriteRenderer renderer))
                _spriteRenderer = renderer;

            _spriteRenderer.color = color;
        }

        public void DisableView()
        {
            gameObject.SetActive(false);
        }

        public void EnableView()
        {
            gameObject.SetActive(true);
        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnObjectTriggerEnter?.Invoke(other);
        }
    }
}