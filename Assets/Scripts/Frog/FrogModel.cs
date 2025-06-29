using UnityEngine;

namespace Frog
{
    public class FrogModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 BoundTopLeft { get; private set; }
        public Vector2 BoundBottomRight { get; private set; }

        public FrogModel(Vector2 position, Vector2 boundTopLeft, Vector2 boundBottomRight)
        {
            Position = position;
            BoundTopLeft = boundTopLeft;
            BoundBottomRight = boundBottomRight;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void Move(Vector2 delta)
        {
            Position += delta;
        }
    }
}