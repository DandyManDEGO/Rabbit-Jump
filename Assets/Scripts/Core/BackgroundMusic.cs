using UnityEngine;
namespace Game.Core
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic _instance;
        private void Awake()
        {
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                AudioSource source = GetComponent<AudioSource>();
                if (source != null && !source.isPlaying)
                {
                    source.Play();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

