using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeMaze.Utils
{
    public class MySceneManager : MonoBehaviour
    {
        [SerializeField] private BusGameManagerSO gameManagerSo;
        [SerializeField] private BusMazeManagerSO mazeManagerSo;
        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadLoadingScene()
        {
            SceneManager.LoadScene("LoadingScene");
        }

        private void ResetSoOnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            gameManagerSo.ResetValues();
            mazeManagerSo.ResetValues();
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
