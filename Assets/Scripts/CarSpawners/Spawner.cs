using UnityEngine;
using Cars;
using System.Collections;
using System.Collections.Generic;

namespace CarSpawners
{
    public class Spawner : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private SpawnData _data;
        [SerializeField] private Vector2 _direction;

        [SerializeField] private CarView _carPrefab;

        private List<CarPresenter> _carEnabled = new();
        private List<CarPresenter> _carsDisabled = new();

        private void OnDisable()
        {
            
        }

        private void Awake()
        {
            if(_data != null)
                StartCoroutine(SpawnColdDown());
        }

        private void Update()
        {
            foreach(CarPresenter car in _carEnabled)
            {
                car.Move(Time.deltaTime);
            }
        }

        public void Initialize(SpawnData data, CarView carPrefab, Vector2 direction)
        {
            _data = data;
            _carPrefab = carPrefab;
            _direction = direction;

            StartCoroutine(SpawnColdDown());
        }

        private void SpawnCar()
        {
            CarPresenter carPresenter = CheckPool();

            if (carPresenter == null)
                carPresenter = CreateNewCar();
            else
                carPresenter.Respawn(transform);
            
            _carEnabled.Add(carPresenter);
            StartCoroutine(SpawnColdDown());
        }

        private CarPresenter CheckPool()
        {
            CarPresenter returnView = null;
            if (_carsDisabled.Count <= 0)
                return returnView;

            returnView = _carsDisabled[0];
            _carsDisabled.Remove(returnView);

            return returnView;
        }

        private CarPresenter CreateNewCar()
        {
            CarView carView = Instantiate(_carPrefab);
            CarModel model = new(transform.position, _direction, _data.Speed, _data.CarColor);
            CarPresenter presenter = new(model , carView);

            carView.transform.parent = transform;
            presenter.OnRequestReturnToPool += HandleDisableCar;
            return presenter;
        }
        
        private void HandleDisableCar(CarPresenter carPresenter)
        {
            if (_carEnabled.Contains(carPresenter))
                _carEnabled.Remove(carPresenter);

            _carsDisabled.Add(carPresenter);
        }

        private IEnumerator SpawnColdDown()
        {
            yield return new WaitForSeconds(_data.SpawnTime);

            SpawnCar();
        }
    }
}