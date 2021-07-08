using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeMaze.Utils
{
    public class ScriptableManager : MonoBehaviour
    {
        [SerializeField] private SOManager soManager;

        private void Awake()
        {
            Debug.Log("Help");
            Debug.Log(PlayerPrefs.GetInt("ScriptablesInitiated") == 0);
            Debug.Log(PlayerPrefs.GetInt("ScriptablesInitiated"));
            if (PlayerPrefs.GetInt("ScriptablesInitiated") == 0)
            {
                Debug.Log("Scriptables initiated");
                soManager.InitScriptables();
                PlayerPrefs.SetInt("ScriptablesInitiated", 1);
            }
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitPlayerPrefs()
        {
            Debug.Log("BeforeLoadScene");
            PlayerPrefs.SetInt("ScriptablesInitiated",0);
            PlayerPrefs.Save();
        }
        private void ResetScriptables(Scene scene, LoadSceneMode loadSceneMode)
        {
            soManager.ResetScriptables();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetScriptables;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ResetScriptables;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("ScriptablesInitiated", 0);
            PlayerPrefs.Save();
        }
    }
}
