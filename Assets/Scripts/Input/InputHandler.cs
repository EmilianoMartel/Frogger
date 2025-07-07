using System;
using UnityEngine;

namespace Input
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        private InputSystem_Actions _inputActions;

        public event Action<Vector2> OnMove;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();

            _inputActions.Player.Move.performed += ctx =>
            {
                OnMove?.Invoke(ctx.ReadValue<Vector2>());
            };

            _inputActions.Player.Move.canceled += ctx =>
            {
                OnMove?.Invoke(Vector2.zero);
            };
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}