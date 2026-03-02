using UnityEngine;
namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _dustEffectPrefab;
        [SerializeField] private Transform _visualTransform;
        private PlayerController _controller;
        private Rigidbody2D _rigidbody;
        private Vector3 _originalScale;
        private Game.Core.AudioService _audioService;
        public void Initialize(PlayerController controller, Game.Core.AudioService audioService)
        {
            _controller = controller;
            _audioService = audioService;
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalScale = _visualTransform != null ? _visualTransform.localScale : transform.localScale;
        }
        private void Update()
        {
            _controller?.Update();
            ApplySquashAndStretch();
        }
        private void ApplySquashAndStretch()
        {
            if (_visualTransform == null) return;
            float velocityY = _rigidbody.linearVelocity.y;
            float stretch = Mathf.Clamp(velocityY * 0.02f, -0.2f, 0.3f);
            Vector3 targetScale = new Vector3(
                _originalScale.x - stretch * 0.5f,
                _originalScale.y + stretch,
                _originalScale.z
            );
            _visualTransform.localScale = Vector3.Lerp(_visualTransform.localScale, targetScale, Time.deltaTime * 10f);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryJump(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            TryJump(collision);
        }
        private void TryJump(Collision2D collision)
        {
            bool hitFromAbove = false;
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).normal.y > 0.4f) 
                {
                    hitFromAbove = true;
                    break;
                }
            }
            if (hitFromAbove && _rigidbody.linearVelocity.y <= 0.1f)
            {
                _audioService?.PlayJump();
                var visual = collision.gameObject.GetComponent<Game.World.PlatformVisual>();
                visual?.PlayBounce();
                if (_dustEffectPrefab != null)
                {
                    Instantiate(_dustEffectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
                }
                if (_visualTransform != null)
                {
                    _visualTransform.localScale = new Vector3(_originalScale.x * 1.4f, _originalScale.y * 0.6f, _originalScale.z);
                }
                _controller.Jump();
            }
        }
    }
}

