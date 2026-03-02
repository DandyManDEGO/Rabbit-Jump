using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Game.Core
{
    public class LoadingCircle : MonoBehaviour
    {
        [SerializeField] private Image[] _loadingElements; 
        [SerializeField] private float _stepTime = 0.1f;
        [SerializeField] private float _minAlpha = 0.2f;
        [SerializeField] private float _maxAlpha = 1f;
        private void Start()
        {
            if (_loadingElements != null && _loadingElements.Length > 0)
            {
                StartCoroutine(AnimateLoading());
            }
        }
        private IEnumerator AnimateLoading()
        {
            int currentIndex = 0;
            foreach (var img in _loadingElements)
            {
                SetAlpha(img, _minAlpha);
            }
            while (true)
            {
                int prevIndex = (currentIndex == 0) ? _loadingElements.Length - 1 : currentIndex - 1;
                SetAlpha(_loadingElements[prevIndex], _minAlpha);
                SetAlpha(_loadingElements[currentIndex], _maxAlpha);
                currentIndex = (currentIndex + 1) % _loadingElements.Length;
                yield return new WaitForSecondsRealtime(_stepTime);
            }
        }
        private void SetAlpha(Image img, float alpha)
        {
            if (img == null) return;
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
}

