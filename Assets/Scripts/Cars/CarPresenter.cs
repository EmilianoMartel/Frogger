using Codice.Client.Common.GameUI;
using System;
using UnityEngine;

namespace Cars
{
    public class CarPresenter
    {
        private CarModel _model;
        private CarView _view;

        public event Action<CarPresenter> OnRequestReturnToPool;
        public event Action<CarPresenter> OnPlayerCollision;

        public CarPresenter(CarModel model, CarView view)
        {
            _model = model;
            _view = view;

            _view.OnObjectTriggerEnter += HandleObjectTriggerEnter;
            _view.UpdatePosition(_model.Position);
        }

        public void Move()
        {
            var movement = _model.MoveDirection * (_model.Speed * Time.deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;

            _model.Move(nextPosition);
            _view.UpdatePosition(_model.Position);
        }

        public void Dispose()
        {
            _view.OnObjectTriggerEnter -= HandleObjectTriggerEnter;
        }

        private void HandleObjectTriggerEnter(Collider2D objectCollider)
        {
            if (objectCollider.CompareTag("Player"))
            {
                OnPlayerCollision?.Invoke(this);
            }
            else if (objectCollider.CompareTag("EndZone"))
            {
                HandleDisabling();
            }
        }

        private void HandleDisabling()
        {
            OnRequestReturnToPool?.Invoke(this);
        }

        private void HandlePlayerCollision()
        {
            OnPlayerCollision?.Invoke(this);
        }
    }
}