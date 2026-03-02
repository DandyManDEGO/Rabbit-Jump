using UnityEngine;
namespace Game.Core
{
    public class ScoreController
    {
        private float _maxHeight;
        private readonly float _platformDistance = 2f;
        public int GetCurrentScore(float playerY)
        {
            if (playerY > _maxHeight)
            {
                _maxHeight = playerY;
            }
            int score = Mathf.FloorToInt(_maxHeight / _platformDistance);
            return Mathf.Max(0, score);
        }
    }
}

