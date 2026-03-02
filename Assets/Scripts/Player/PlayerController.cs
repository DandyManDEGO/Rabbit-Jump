using UnityEngine;
using Game.Core;
namespace Game.Player
{
    public class PlayerController
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IInputProvider _input;
        private readonly float _moveSpeed = 5f;
        private readonly float _jumpForce = 10f;
        private readonly float _screenBoundary = 3f;
        public Vector2 Position => _rigidbody2D.position;
        public PlayerController(Rigidbody2D rigidbody2D, IInputProvider input)
        {
            _rigidbody2D = rigidbody2D;
            _input = input;
        }
        public void Update()
        {
            HandleMovement();
            HandleScreenWrapping();
        }
        public void Jump()
        {
            if (_rigidbody2D.linearVelocity.y <= 0.5f)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
            }
        }
        private void HandleMovement()
        {
            float horizontalInput = _input.GetHorizontalInput();
            _rigidbody2D.linearVelocity = new Vector2(horizontalInput * _moveSpeed, _rigidbody2D.linearVelocity.y);
        }
        private void HandleScreenWrapping()
        {
            Vector3 position = _rigidbody2D.transform.position;
            if (position.x > _screenBoundary) position.x = -_screenBoundary;
            else if (position.x < -_screenBoundary) position.x = _screenBoundary;
            _rigidbody2D.transform.position = position;
        }
    }
}

