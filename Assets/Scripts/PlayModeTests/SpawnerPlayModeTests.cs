using CarSpawners;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using Cars;

namespace PlayModeTests
{
    public class SpawnerPlayModeTests
    {
        private GameObject _spawnerGO;
        private Spawner _spawner;
        private SpawnData _data;
        private CarView _carPrefab;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            _spawnerGO = new GameObject("Spawner");
            _spawner = _spawnerGO.AddComponent<Spawner>();

            // Mock SpawnData
            CarData carData = ScriptableObject.CreateInstance<CarData>();
            carData.Initialize(Color.blue, 1);

            _data = ScriptableObject.CreateInstance<SpawnData>();
            _data.Initialize(carData, 1f, 2f, 2f);

            // Mock CarView prefab
            GameObject carGO = new GameObject("CarPrefab");
            
            _carPrefab = carGO.AddComponent<CarView>();
            _carPrefab.gameObject.AddComponent<SpriteRenderer>();

            // Simulate Serialized Fields
            typeof(Spawner).GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_spawner, _data);
            typeof(Spawner).GetField("_carPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_spawner, _carPrefab);
            typeof(Spawner).GetField("_direction", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_spawner, Vector2.right);

            _spawner.Initialize(_data, _carPrefab.GetComponent<CarView>(), new Vector2(0, 1));

            yield return null; // Wait one frame to allow Awake
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_spawnerGO != null)
                GameObject.Destroy(_spawnerGO);

            if (_carPrefab != null)
                GameObject.Destroy(_carPrefab.gameObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Spawner_CreatesCarAfterCooldown()
        {
            // Wait longer than the spawner cooldown
            yield return new WaitForSeconds(_data.SpawnTime + 0.1f);

            var carList = (List<CarPresenter>)typeof(Spawner)
                .GetField("_carEnabled", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(_spawner);

            Assert.IsNotNull(carList);
            Assert.AreEqual(1, carList.Count);
        }

        [UnityTest]
        public IEnumerator Spawner_HandlesCarReturnToPool()
        {
            yield return new WaitForSeconds(_data.SpawnTime + 0.1f);

            var carList = (List<CarPresenter>)typeof(Spawner)
                .GetField("_carEnabled", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(_spawner);

            Assert.IsNotNull(carList, "_carEnabled list is null");
            Assert.Greater(carList.Count, 0, "No cars were spawned!");

            CarPresenter car = carList[0];

            car.GetType()
                .GetMethod(
                    "HandleDisabling",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                )
                .Invoke(car, null);

            var disabledList = (List<CarPresenter>)typeof(Spawner)
                .GetField("_carsDisabled", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(_spawner);

            Assert.AreEqual(1, disabledList.Count, "Car was not returned to the pool");
            Assert.AreEqual(0, carList.Count, "Car still active in _carEnabled list");
        }
    }
}