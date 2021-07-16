using UnityEngine;

namespace SnakeMaze.Player
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptables/User/UserDataControllerSO")]
    public class UserDataControllerSO : ScriptableObject
    {
        [SerializeField] private int _highScore;

        public int HighScore
        {
            get => _highScore;
            set => _highScore = value;
        }

        public void LoadData(string jsonData)
        {
            Debug.Log("Loading Data");
            
            var userData = new UserData();
            JsonUtility.FromJsonOverwrite(jsonData, userData);

            _highScore = userData.Score;
            Debug.Log("Server HighScore: " + userData.Score);
            Debug.Log("Server json HighScore: " + jsonData);
            Debug.Log("Local points: " + HighScore);
        }


        private class UserData
        {
            public int Score;
        }
    }
}