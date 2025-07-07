using UnityEngine;
using Input;
using System;

namespace Frog
{
    public class FrogPresenter
    {
        private FrogModel _model;
        private IFrogView _view;
        private float _moveSpeed;

        private string _carTag = "Car";
        private string _endZone = "FinalZone";

        private Vector2 _currentDirection = Vector2.zero;

        public event Action OnCarTriggerEntred;
        public event Action OnFinalZoneEntered;

        public FrogPresenter(FrogModel model, IFrogView playerView, IInputHandler inputHandler, float moveSpeed)
        {
            _model = model;
            _view = playerView;
            _moveSpeed = moveSpeed;

            _view.OnTriggerEnterEvent += HandleCheckCarCollition;
            inputHandler.OnMove += OnMove;
        }

        public void Dispose(InputHandler inputHandler)
        {
            _view.OnTriggerEnterEvent -= HandleCheckCarCollition;
            inputHandler.OnMove -= OnMove;
        }

        public void Update(float deltaTime)
        {
            UpdateMovement(deltaTime);
        }

        public void SetSpawnPosition(Vector2 spawnPosition)
        {
            _model.SetPosition(spawnPosition);
            _view.UpdatePosition(_model.Position);
        }

        private void OnMove(Vector2 direction)
        {
            _currentDirection = direction.sqrMagnitude > 0.01f ? direction.normalized : Vector2.zero;
        }

        private void HandleCheckCarCollition(Collider2D collider)
        {
            if (collider.CompareTag(_carTag)) OnCarTriggerEntred?.Invoke();
            else if (collider.CompareTag(_endZone)) OnFinalZoneEntered?.Invoke();
        }

        private void UpdateMovement(float deltaTime)
        {
            var movement = _currentDirection * (_moveSpeed * deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;

            nextPosition.x = Mathf.Clamp(nextPosition.x, _model.BoundTopLeft.x, _model.BoundBottomRight.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, _model.BoundBottomRight.y, _model.BoundTopLeft.y);

            _model.Move(nextPosition - currentPosition);
            _view.UpdatePosition(_model.Position);
        }
    }
}