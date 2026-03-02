using UnityEngine;
using System.Collections;
namespace Game.World
{
    public class PlatformVisual : MonoBehaviour
    {
        private Vector3 _originalPos;
        private Coroutine _bounceCoroutine;
        private void Start()
        {
            _originalPos = transform.localPosition;
        }
        public void PlayBounce()
        {
            if (_bounceCoroutine != null) StopCoroutine(_bounceCoroutine);
            _bounceCoroutine = StartCoroutine(BounceRoutine());
        }
        private IEnumerator BounceRoutine()
        {
            float duration = 0.15f;
            float dipAmount = 0.2f;
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                transform.localPosition = _originalPos + Vector3.down * Mathf.Lerp(0, dipAmount, t);
                yield return null;
            }
            elapsed = 0;
            while (elapsed < duration * 2)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (duration * 2);
                float offset = Mathf.Sin(t * Mathf.PI) * dipAmount;
                transform.localPosition = _originalPos - Vector3.down * offset;
                yield return null;
            }
            transform.localPosition = _originalPos;
        }
    }
}

