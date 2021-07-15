using UnityEngine;

namespace SnakeMaze.Player
{
    public class UserDataController
    {
        private int _highScore;

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
            Debug.Log("Server HighScore: " + userData.Score);
            Debug.Log("Server json HighScore: " + jsonData);
        }


        private class UserData
        {
            public int Score;
        }
    }
}