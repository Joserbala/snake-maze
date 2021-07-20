using SnakeMaze.Enums;
using SnakeMaze.PlayFab;
using SnakeMaze.User;
using UnityEngine;

namespace SnakeMaze.SO.UserDataSO
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Scriptables/User/UserDataControllerSO")]
    public class UserDataControllerSO : ScriptableObject
    {
        [SerializeField] private EventSO playFabServerResponse;
        [SerializeField] private BusSelectSkinSO busSelectSkinSo;
        
        private ScoreData _scoreData = new ScoreData();
        private EconomyData _economyData = new EconomyData();
        private CurrentSkinData _skinData = new CurrentSkinData();
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

        public SnakeSkinEnum CurrentSnakeSkin
        {
            get => SkinEnumUtils.StringToSnakeEnum(_skinData.CurrentSnakeSkin);
            set => _skinData.CurrentSnakeSkin = value.ToString();
        }
        public MazeSkinEnum CurrentMazeSkin
        {
            get => SkinEnumUtils.StringToMazeEnum(_skinData.CurrentMazeSkin);
            set => _skinData.CurrentMazeSkin = value.ToString();
        }

        private void SetCurrentSnakeSkin(SnakeSkinEnum snakeSkin)
        {
            CurrentSnakeSkin = snakeSkin;
        }
        private void SetCurrentMazeSkin(MazeSkinEnum mazeSkin)
        {
           CurrentMazeSkin = mazeSkin;
        }
        private void SetCurrentSnakeSkin(string snakeSkin)
        {
            _skinData.CurrentSnakeSkin = snakeSkin;
        }
        private void SetCurrentMazeSkin(string mazeSkin)
        {
            _skinData.CurrentMazeSkin = mazeSkin;
        }

        public void LoadData(LoginDataResult loginData)
        {
            JsonUtility.FromJsonOverwrite(loginData.loginData.readOnlyData["HighScore"].Value, _scoreData);
            JsonUtility.FromJsonOverwrite(loginData.loginData.readOnlyData["Skins"].Value, _skinData);

            _economyData.SetEconomyData(loginData.loginData.currency);
            
            Debug.Log("SoftCoins: " + SoftCoins);
            Debug.Log("HardCoins: " + HardCoins);
            Debug.Log("CurrentSnakeSkin: " + _skinData.CurrentSnakeSkin);
            Debug.Log("CurrentMazeSkin: " + _skinData.CurrentMazeSkin);
            playFabServerResponse.CurrentAction.Invoke();
        }

        private void OnEnable()
        {
            busSelectSkinSo.OnSnakeSkinSelect += SetCurrentSnakeSkin;
            busSelectSkinSo.OnMazeSkinSelect += SetCurrentMazeSkin;
        }
        private void OnDisable()
        {
            busSelectSkinSo.OnSnakeSkinSelect -= SetCurrentSnakeSkin;
            busSelectSkinSo.OnMazeSkinSelect -= SetCurrentMazeSkin;
        }
    }
}