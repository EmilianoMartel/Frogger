using System;
using UnityEngine;

namespace Cars
{
    public class CarView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public virtual event Action<Collider2D> OnObjectTriggerEnter;

        public virtual void SetNewColor(Color color)
        {
            if(_spriteRenderer == null && transform.TryGetComponent(out SpriteRenderer renderer))
                _spriteRenderer = renderer;

            _spriteRenderer.color = color;
        }

        public virtual void DisableView()
        {
            gameObject.SetActive(false);
        }

        public virtual void EnableView()
        {
            gameObject.SetActive(true);
        }

        public virtual void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnObjectTriggerEnter?.Invoke(other);
        }
    }
}