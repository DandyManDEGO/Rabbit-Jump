using UnityEngine;
namespace Game.Core
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioClip _jumpSound;
        public void PlayJump()
        {
            if (_sfxSource != null && _jumpSound != null)
            {
                _sfxSource.PlayOneShot(_jumpSound);
            }
        }
    }
}

