using Input;
using System;
using UnityEngine;
using Input;

namespace Tests
{
    public class MockInputHandler : InputHandler
    {
        public override event Action<Vector2> OnMove;

        public void SimulateMove(Vector2 direction)
        {
            OnMove?.Invoke(direction);
        }
    }
}
