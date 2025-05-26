using UnityEngine;

namespace Frog
{
    public class FrogView : MonoBehaviour
    {
        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}