using SnakeMaze.SO;
using SnakeMaze.SO.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeMaze.Utils
{
    public class MySceneManager : MonoBehaviour
    {
        [SerializeField] private SOManager soManager;

        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadLoadingScene()
        {
            SceneManager.LoadScene("LoadingScene");
        }

        public void LoadScene(string sceneToLoad = "LoadingScene")
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        private void ResetSoOnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            soManager.ResetScriptables();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetSoOnLoadScene;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ResetSoOnLoadScene;
        }
    }
}
