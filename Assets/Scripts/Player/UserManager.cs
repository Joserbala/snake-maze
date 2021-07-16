using SnakeMaze.SO;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.SO.UserData;
using UnityEngine;

namespace SnakeMaze.Player
{
    public class UserManager : MonoBehaviour
    {
        [SerializeField] private EventSO logInEvent;
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
                    LoadUserData(loginData.LoginData.ReadOnlyData["HighScore"].Value);
                    Debug.Log("Server HighScore: " + loginData.LoginData.ReadOnlyData["HighScore"].Value);
                },
                null);
        }

        private void LoadUserData(string userDataJson)
        {
            userDataControllerSo.LoadData(userDataJson);
        }

        private void UpdateScoreData()
        {
            var points = player.Points;
            Debug.Log("local points: " + userDataControllerSo.HighScore);
            Debug.Log("new socre: " + points);
            if (points <= userDataControllerSo.HighScore) return;

            playFabManager.UpdateUserScore(player.Points);
            userDataControllerSo.HighScore = points;
            Debug.Log(" userDataControllerSo.HighScore: " +  userDataControllerSo.HighScore);
            Debug.Log("Score updated");
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        private void OnEnable()
        {
            busGameManagerSo.EndGame += UpdateScoreData;
        }

        private void OnDisable()
        {
            busGameManagerSo.EndGame -= UpdateScoreData;
        }
    }
}