using UnityEngine;
using Input;
using System;

namespace Frog
{
    public class FrogPresenter
    {
        private FrogModel _model;
        private FrogView _view;
        private float _moveSpeed;

        private string _carTag = "Car";
        private string _endZone = "FinalZone";

        private Vector2 _currentDirection = Vector2.zero;

        public event Action OnCarTriggerEntred;
        public event Action OnFinalZoneEntered;

        public FrogPresenter(FrogModel model, FrogView playerView, InputHandler inputHandler, float moveSpeed)
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

        public void Update()
        {
            UpdateMovement();
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

        private void UpdateMovement()
        {
            var movement = _currentDirection * (_moveSpeed * Time.deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;

            nextPosition.x = Mathf.Clamp(nextPosition.x, _model.BoundTopLeft.x, _model.BoundBottomRight.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, _model.BoundBottomRight.y, _model.BoundTopLeft.y);

            _model.Move(nextPosition - currentPosition);
            _view.UpdatePosition(_model.Position);
        }
    }
}