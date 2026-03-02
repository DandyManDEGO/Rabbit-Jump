using UnityEngine;
using Game.Player;
using Game.Core;
using Game.World;
namespace Game.Entry
{
        public class GameEntryPoint : MonoBehaviour
        {
            public static GameEntryPoint Instance { get; private set; }
            [Header("References")]
            [SerializeField] private PlayerView _playerView;
            [SerializeField] private Rigidbody2D _playerRigidbody;
            [SerializeField] private GameObject[] _platformPrefabs;
            [SerializeField] private CameraFollow _cameraFollow;
            [SerializeField] private GameOverUI _gameOverUI;
            [SerializeField] private ScoreView _scoreView;
            [SerializeField] private AudioService _audioService;
            private PlayerController _playerController;
            private PlatformGenerator _platformGenerator;
            private ScoreController _scoreController;
            private RecordService _recordService;
            private bool _isGameOver;
            private int _currentScore;
            private void Awake()
            {
                Application.targetFrameRate = 90;
                Instance = this;
                Initialize();
            }
            private void OnDestroy()
            {
                if (Instance == this) Instance = null;
            }
            public void SaveCurrentRecord()
            {
                if (_recordService != null && _currentScore > 0)
                {
                    _recordService.SaveRecord(_currentScore);
                }
            }
            private void Update()
        {
            if (_isGameOver) return;
            float playerY = _playerController.Position.y;
            float cameraY = _cameraFollow.transform.position.y;
            _platformGenerator?.Update(cameraY);
            _currentScore = _scoreController.GetCurrentScore(playerY);
            _scoreView.UpdateScore(_currentScore);
            if (playerY < cameraY - 7f)
            {
                EndGame();
            }
        }
        private void Initialize()
        {
            Time.timeScale = 1f;
            _isGameOver = false;
            _currentScore = 0;
            IInputProvider input = new MobileInput();
            _playerController = new PlayerController(_playerRigidbody, input);
            _playerView.Initialize(_playerController, _audioService);
            _platformGenerator = new PlatformGenerator(_platformPrefabs, transform);
            _platformGenerator.Initialize(10);
            _scoreController = new ScoreController();
            _scoreView.UpdateScore(0);
            _recordService = new RecordService();
            _cameraFollow.SetTarget(_playerRigidbody.transform);
            _gameOverUI.Initialize();
        }
        private void EndGame()
        {
            Handheld.Vibrate();
            _isGameOver = true;
            SaveCurrentRecord();
            _gameOverUI.Show(_currentScore);
        }
    }
}

