using UnityEngine;
namespace Game.World
{
    public class MovingPlatform : MonoBehaviour
    {
        private float _speed;
        private float _range;
        private float _startPos;
        private float _timeOffset;
        public void Initialize(float speed, float range)
        {
            _speed = speed;
            _range = range;
            _startPos = transform.position.x;
            _timeOffset = Random.Range(0f, Mathf.PI * 2f);
        }
        private void Update()
        {
            float movement = Mathf.Sin(Time.time * _speed + _timeOffset) * _range;
            float newX = _startPos + movement;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}

