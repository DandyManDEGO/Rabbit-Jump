using UnityEngine;
namespace Game.Core
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;
        private float _smoothSpeed = 5f;
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        private void LateUpdate()
        {
            if (_target == null) return;
            if (_target.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, _target.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, _smoothSpeed * Time.deltaTime);
            }
        }
    }
}

