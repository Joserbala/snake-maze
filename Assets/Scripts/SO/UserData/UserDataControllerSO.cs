using SnakeMaze.PlayFab;
using SnakeMaze.User;
using UnityEngine;

namespace SnakeMaze.SO.UserDataSO
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptables/User/UserDataControllerSO")]
    public class UserDataControllerSO : ScriptableObject
    {
        [SerializeField] private EventSO playFabServerResponse;
        
        private ScoreData _scoreData = new ScoreData();
        private EconomyData _economyData = new EconomyData();
        private string _nickName;

        public string NickName
        {
            get => _nickName;
            set => _nickName = value;
        }

        public int HighScore
        {
            get => _scoreData.Score;
            set => _scoreData.Score = value;
        }

        public int SoftCoins
        {
            get => _economyData.SoftCoin;
            set => _economyData.SoftCoin = value;
        }
        public int HardCoins
        {
            get => _economyData.HardCoin;
            set => _economyData.HardCoin = value;
        }

        public void LoadData(LoginDataResult loginData)
        {
            JsonUtility.FromJsonOverwrite(loginData.loginData.readOnlyData["HighScore"].Value, _scoreData);
            _economyData.SetEconomyData(loginData.loginData.currency);
            
            Debug.Log("SoftCoins: " + SoftCoins);
            Debug.Log("HardCoins: " + HardCoins);
            playFabServerResponse.CurrentAction.Invoke();
        }
    }
}