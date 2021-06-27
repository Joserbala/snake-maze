using SnakeMaze.SO;
using SnakeMaze.SO.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeMaze.Utils
{
    public class MySceneManager : MonoBehaviour
    {
        [SerializeField] private BusGameManagerSO gameManagerSo;
        [SerializeField] private BusMazeManagerSO mazeManagerSo;
        [SerializeField] private SoundEmitterPoolSO soundEmitterPoolSo;
        [SerializeField] private PlayerVariableSO playerVariableSo;

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
            gameManagerSo?.ResetValues();
            mazeManagerSo?.ResetValues();
            soundEmitterPoolSo?.ResetValues();
            playerVariableSo?.ResetValues();
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
