using UnityEngine;
using Cars;

namespace CarSpawners
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "GameplayData/SpawnData")]
    public class SpawnData : ScriptableObject
    {
        [Header("CarData")]
        [SerializeField] private CarData _car;

        [Header("SpawnData")]
        [Tooltip("This speed is added to the car base speed")]
        [SerializeField] private float _spawnSpeedSum = 0f;
        [SerializeField] private float _spawnMinTime = 0f;
        [SerializeField] private float _spawnMaxTime = 1f;

        public void Initialize(CarData car, float speed, float minTime, float maxTime)
        {
            _car = car;
            _spawnSpeedSum = speed;
            _spawnMinTime = minTime;
            _spawnMaxTime = maxTime;
        }

        public Color CarColor { get { return _car.CarColor; } }
        public float Speed { get { return _spawnSpeedSum + _car.CarSpeed; } }
        public float SpawnTime { get { return UnityEngine.Random.Range(_spawnMinTime, _spawnMaxTime); } }
    }
}