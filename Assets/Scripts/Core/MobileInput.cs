using UnityEngine;
namespace Game.Core
{
    public class MobileInput : IInputProvider
    {
        private float _lastMouseX;
        private bool _isDragging;
        private readonly float _sensitivity = 1.5f;
        public float GetHorizontalInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastMouseX = Input.mousePosition.x;
                _isDragging = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }
            if (_isDragging)
            {
                float currentMouseX = Input.mousePosition.x;
                float delta = (currentMouseX - _lastMouseX) / Screen.width;
                _lastMouseX = currentMouseX;
                return Mathf.Clamp(delta * 100f * _sensitivity, -1f, 1f);
            }
            return 0f;
        }
    }
}

