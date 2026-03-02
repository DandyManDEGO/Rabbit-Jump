using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
namespace Game.Core
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Text _finalScoreText;
        [Header("Drop Objects (In Order)")]
        [SerializeField] private RectTransform _backToMenuButton;
        [SerializeField] private RectTransform _gameOverText;
        [SerializeField] private RectTransform _scoreArea;
        [SerializeField] private RectTransform _playAgainButton;
        [Header("Animation Settings")]
        [SerializeField] private float _dropDistance = 1000f;
        [SerializeField] private float _dropDuration = 0.6f;
        [SerializeField] private float _delayBetween = 0.15f;
        [SerializeField] private Ease _dropEase = Ease.OutBack;
        private Vector2[] _originalPositions;
        public void Initialize()
        {
            _panel.SetActive(false);
            _restartButton.onClick.AddListener(RestartGame);
            _originalPositions = new Vector2[4];
            if (_backToMenuButton != null) _originalPositions[0] = _backToMenuButton.anchoredPosition;
            if (_gameOverText != null) _originalPositions[1] = _gameOverText.anchoredPosition;
            if (_scoreArea != null) _originalPositions[2] = _scoreArea.anchoredPosition;
            if (_playAgainButton != null) _originalPositions[3] = _playAgainButton.anchoredPosition;
        }
        public void Show(int finalScore)
        {
            if (_finalScoreText != null)
            {
                _finalScoreText.text = finalScore.ToString();
            }
            Time.timeScale = 0f;
            _panel.SetActive(true);
            PrepareObject(_backToMenuButton, 0);
            PrepareObject(_gameOverText, 1);
            PrepareObject(_scoreArea, 2);
            PrepareObject(_playAgainButton, 3);
            AnimateDrop(_backToMenuButton, 0, 0f);
            AnimateDrop(_gameOverText, 1, _delayBetween);
            AnimateDrop(_scoreArea, 2, _delayBetween * 2);
            AnimateDrop(_playAgainButton, 3, _delayBetween * 3);
        }
        private void PrepareObject(RectTransform rt, int index)
        {
            if (rt == null) return;
            rt.anchoredPosition = new Vector2(_originalPositions[index].x, _originalPositions[index].y + _dropDistance);
        }
        private void AnimateDrop(RectTransform rt, int index, float delay)
        {
            if (rt == null) return;
            rt.DOAnchorPos(_originalPositions[index], _dropDuration)
                .SetEase(_dropEase)
                .SetDelay(delay)
                .SetUpdate(true);
        }
        private void RestartGame()
        {
            Time.timeScale = 1f;
            GlobalSceneLoader.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

