using UnityEngine;

namespace SnakeMaze.SO.UserData
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptables/User/UserDataControllerSO")]
    public class UserDataControllerSO : ScriptableObject
    {
        [SerializeField] private  int _highScore;


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
        }

        private void OnDisable()
        {
            Debug.Log("Disbling UserData with score: " + _highScore);
        }

        private void OnEnable()
        {
            Debug.Log("Enabling UserData with score: " + _highScore);
        }
        
        private class UserData
        {
            public int Score;
        }
    }
}