using UnityEngine;

namespace Cars
{
    [CreateAssetMenu(fileName = "CarData", menuName = "GameplayData/CarData")]
    public class CarData : ScriptableObject
    {
        [SerializeField] private Color _carColor = Color.white;
        [SerializeField] private float _carSpeed = 5f;

        public void Initialize(Color color, float speed)
        {
            _carSpeed = speed;
            _carColor = color;
        }

        public Color CarColor { get { return _carColor; } }
        public float CarSpeed { get { return _carSpeed; } }
    }
}