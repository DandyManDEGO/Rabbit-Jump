using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
namespace Game.Core
{
    public class GlobalSceneLoader : MonoBehaviour
    {
        private static string _targetSceneName;
        public static void LoadScene(string sceneName)
        {
            _targetSceneName = sceneName;
            SceneManager.LoadScene("LoaderScene");
        }
        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "LoaderScene")
            {
                StartCoroutine(LoadTargetScene());
            }
        }
        private IEnumerator LoadTargetScene()
        {
            yield return new WaitForSecondsRealtime(2f);
            SceneManager.LoadScene(_targetSceneName);
        }
    }
}

