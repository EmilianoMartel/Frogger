using Input;
using System;
using UnityEngine;
using Input;

namespace Tests
{
    public class MockInputHandler : IInputHandler
    {
        public event Action<Vector2> OnMove;

        public void SimulateMove(Vector2 direction)
        {
            OnMove?.Invoke(direction);
        }
    }
}