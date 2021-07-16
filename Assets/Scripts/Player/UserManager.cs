using SnakeMaze.SO;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.Utils;
using UnityEngine;

namespace SnakeMaze.Player
{
    public class UserManager : MonoBehaviour
    {
        [SerializeField] private EventSO logInEvent;
        [SerializeField] private PlayFabManagerSO playFabManager;
        
        
        private UserDataController _userDataController = new UserDataController();
        

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
            _userDataController.LoadData(userDataJson);

        }
    }
}
