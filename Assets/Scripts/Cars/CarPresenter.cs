using Codice.Client.Common.GameUI;
using System;
using UnityEditor.Search;
using UnityEngine;

namespace Cars
{
    public class CarPresenter
    {
        private CarModel _model;
        private ICarView _view;

        private string _playerTag = "Player";
        private string _endZoneString = "EndZone";

        public event Action<CarPresenter> OnRequestReturnToPool;
        
        public CarPresenter(CarModel model, ICarView view)
        {
            _model = model;
            _view = view;

            _view.SetNewColor(_model.Color);
            _view.OnObjectTriggerEnter += HandleObjectTriggerEnter;
            _view.UpdatePosition(_model.Position);
        }

        public void Move(float deltaTime)
        {
            var movement = _model.MoveDirection * (_model.Speed * deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;

            _model.Move(nextPosition);
            _view.UpdatePosition(_model.Position);
        }

        public void Dispose()
        {
            _view.OnObjectTriggerEnter -= HandleObjectTriggerEnter;
        }

        public void Respawn(Transform position)
        {
            _model.Move(position.position);
            _view.EnableView();
            _view.UpdatePosition(_model.Position);
        }

        private void HandleObjectTriggerEnter(Collider2D objectCollider)
        {
            if (objectCollider.CompareTag(_playerTag) || objectCollider.CompareTag(_endZoneString))
                HandleDisabling();
        }

        private void HandleDisabling()
        {
            OnRequestReturnToPool?.Invoke(this);
            _view.DisableView();
        }
    }
}