using UnityEngine;
using System.Collections.Generic;
namespace Game.World
{
    public class PlatformGenerator
    {
        private readonly GameObject[] _platformPrefabs;
        private readonly Transform _parent;
        private readonly List<GameObject> _activePlatforms = new List<GameObject>();
        private float _lastY = -4f;
        private readonly float _distanceBetween = 2f;
        private readonly float _width = 2.5f;
        private readonly float _viewDistance = 12f;
        private readonly float _deleteThreshold = 5f;
        private readonly float _movingPlatformChance = 0.2f;
        public PlatformGenerator(GameObject[] prefabs, Transform parent)
        {
            _platformPrefabs = prefabs;
            _parent = parent;
        }
        public void Initialize(int count)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnPlatform(false);
            }
            for (int i = 3; i < count; i++)
            {
                SpawnPlatform();
            }
        }
        public void Update(float cameraY)
        {
            if (_lastY < cameraY + _viewDistance)
            {
                SpawnPlatform();
            }
            CleanupPlatforms(cameraY);
        }
        private void SpawnPlatform(bool allowMoving = true)
        {
            _lastY += _distanceBetween;
            GameObject prefabToSpawn = _platformPrefabs[Random.Range(0, _platformPrefabs.Length)];
            float platformHalfWidth = 0.5f;
            BoxCollider2D collider = prefabToSpawn.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                platformHalfWidth = (collider.size.x * prefabToSpawn.transform.localScale.x) / 2f;
            }
            float safeWidth = _width - platformHalfWidth;
            float randomX = Random.Range(-safeWidth, safeWidth);
            Vector3 position = new Vector3(randomX, _lastY, 0);
            GameObject platform = Object.Instantiate(prefabToSpawn, position, Quaternion.identity, _parent);
            platform.AddComponent<PlatformVisual>();
            if (allowMoving && Random.value < _movingPlatformChance)
            {
                var mover = platform.AddComponent<MovingPlatform>();
                mover.Initialize(Random.Range(0.8f, 1.2f), Random.Range(0.4f, 0.8f));
            }
            _activePlatforms.Add(platform);
        }
        private void CleanupPlatforms(float cameraY)
        {
            for (int i = _activePlatforms.Count - 1; i >= 0; i--)
            {
                if (_activePlatforms[i].transform.position.y < cameraY - _deleteThreshold)
                {
                    Object.Destroy(_activePlatforms[i]);
                    _activePlatforms.RemoveAt(i);
                }
            }
        }
    }
}

