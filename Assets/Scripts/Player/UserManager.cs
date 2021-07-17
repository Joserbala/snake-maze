using SnakeMaze.PlayFab;
using SnakeMaze.SO;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.SO.UserDataSO;
using SnakeMaze.Utils;
using UnityEngine;

namespace SnakeMaze.Player
{
    public class UserManager : MonoBehaviour
    {
        [SerializeField] private EventSO logInEvent;
        [SerializeField] private EventSO playFabServerResponse;
        [SerializeField] private UserDataControllerSO userDataControllerSo;
        [SerializeField] private PlayFabManagerSO playFabManager;
        [SerializeField] private BusGameManagerSO busGameManagerSo;
        [SerializeField] private PlayerVariableSO player;


        private void Awake()
        {
            logInEvent.CurrentAction += OnServerLogin;
        }

        private void OnDestroy()
        {
            logInEvent.CurrentAction -= OnServerLogin;
        }

        private void OnServerLogin()
        {
            playFabManager.GetLoginData(
                loginData =>
                {
                    LoadUserData(loginData);
                    Debug.Log("Server HighScore: " + loginData.LoginData.ReadOnlyData["HighScore"].Value);
                },
                () => { playFabServerResponse.CurrentAction?.Invoke(); });
        }

        private void LoadUserData(LoginDataResult loginData)
        {
            userDataControllerSo.LoadData(loginData);
        }

        private void OnWinUpdate()
        {
            UpdateScoreData();
            UpdateCurrencyData(true);
        }

        private void OnLooseUpdate()
        {
            UpdateScoreData();
            UpdateCurrencyData(false);
        }

        private void UpdateScoreData()
        {
            var points = player.Points;
            Debug.Log("local points: " + userDataControllerSo.HighScore);
            Debug.Log("new socre: " + points);
            if (points <= userDataControllerSo.HighScore) return;

            playFabManager.UpdateScore(player.Points);
            userDataControllerSo.HighScore = points;
            Debug.Log(" userDataControllerSo.HighScore: " + userDataControllerSo.HighScore);
            Debug.Log("Score updated");
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        private void UpdateCurrencyData(bool hasWon)
        {
            var coins = EconomyManager.SetCoinsFromPoint(hasWon, player.Points);
            userDataControllerSo.SoftCoins += coins;
            playFabManager.AddSCCurrency( coins);
            Debug.Log("Coins received: " + coins);
            Debug.Log("New coins: " + userDataControllerSo.SoftCoins);
            
        }


        private void OnEnable()
        {
            busGameManagerSo.EndGame += OnLooseUpdate;
            busGameManagerSo.WinGame += OnWinUpdate;
        }

        private void OnDisable()
        {
            busGameManagerSo.EndGame -= OnLooseUpdate;
            busGameManagerSo.WinGame -= OnWinUpdate;
        }
    }
}