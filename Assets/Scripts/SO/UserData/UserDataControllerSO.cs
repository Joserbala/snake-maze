using SnakeMaze.User;
using UnityEngine;

namespace SnakeMaze.SO.UserDataSO
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptables/User/UserDataControllerSO")]
    public class UserDataControllerSO : ScriptableObject
    {
        [SerializeField] private int _highScore;
        [SerializeField] private EventSO playFabServerResponse;


        public int HighScore
        {
            get => _highScore;
            set => _highScore = value;
        }

        public void LoadData(string jsonData)
        {
            var userData = new UserData();
            JsonUtility.FromJsonOverwrite(jsonData, userData);

            _highScore = userData.Score;
            playFabServerResponse.CurrentAction.Invoke();
        }

        private void OnDisable()
        {
            Debug.Log("Disbling UserData with score: " + _highScore);
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
            Debug.Log("Enabling UserData with score: " + _highScore);
        }
    }
}