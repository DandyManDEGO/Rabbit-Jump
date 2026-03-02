using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Game.Core
{
    public class ButtonManager : MonoBehaviour
    {
        [Header("Pause UI")]
        [SerializeField] private GameObject _pauseCanvas;
        [SerializeField] private RectTransform _pausePopup; 
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [Header("Animation Settings")]
        [SerializeField] private float _overshootScale = 1.2f;
        [SerializeField] private float _appearDuration = 1.0f;
        [SerializeField] private float _settleDuration = 0.5f;
        [Header("Navigation")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitToMenuButton;
        [SerializeField] private Button _exitToMenuGameOverButton;
        private void Start()
        {
            if (_pauseButton != null) _pauseButton.onClick.AddListener(PauseGame);
            if (_resumeButton != null) _resumeButton.onClick.AddListener(ResumeGame);
            if (_startButton != null) _startButton.onClick.AddListener(StartNewGame);
            if (_exitToMenuButton != null) _exitToMenuButton.onClick.AddListener(ExitToMenu);
            if (_exitToMenuGameOverButton != null) _exitToMenuGameOverButton.onClick.AddListener(ExitToMenu);
            if (_pauseCanvas != null) _pauseCanvas.SetActive(false);
        }
        public void StartNewGame()
        {
            GlobalSceneLoader.LoadScene("GameScene");
        }
        public void PauseGame()
        {
            Time.timeScale = 0f;
            if (_pauseCanvas != null)
            {
                _pauseCanvas.SetActive(true);
                if (_pausePopup != null)
                {
                    _pausePopup.localScale = Vector3.zero;
                    _pausePopup.DOScale(_overshootScale, _appearDuration)
                        .SetEase(Ease.OutQuad)
                        .SetUpdate(true)
                        .OnComplete(() => 
                        {
                            _pausePopup.DOScale(1f, _settleDuration)
                                .SetEase(Ease.InOutQuad)
                                .SetUpdate(true);
                        });
                }
            }
        }
        public void ResumeGame()
        {
            Time.timeScale = 1f;
            if (_pauseCanvas != null) _pauseCanvas.SetActive(false);
        }
        public void ExitToMenu()
        {
            if (Game.Entry.GameEntryPoint.Instance != null)
            {
                Game.Entry.GameEntryPoint.Instance.SaveCurrentRecord();
            }
            Time.timeScale = 1f;
            GlobalSceneLoader.LoadScene("MainMenuScene");
        }
    }
}

