using UnityEngine;

namespace Cars
{
    public class CarModel
    {
        public Color Color { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public float Speed { get; private set; }

        public CarModel(Vector2 startPosition, Vector2 moveDirection, float speed, Color color)
        {
            Position = startPosition;
            MoveDirection = moveDirection.normalized;
            Speed = speed;
            Color = color;
        }

        public void Move(Vector2 nextPosition)
        {
            Position = nextPosition;
        }
    }
}