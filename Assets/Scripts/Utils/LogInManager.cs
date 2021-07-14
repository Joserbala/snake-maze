using System;
using System.Collections.Generic;
using SnakeMaze.SO.PlayFabManager;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace SnakeMaze.Utils
{
    public class LogInManager : MonoBehaviour
    {
        //Event para que este evento no se pueda llamar desde otra clase, solo suscribirse.
        public static event Action onServerLogin;
        [SerializeField] private PlayFabManagerSO playFabManagerSo;
        [SerializeField] private GameObject createAccountPanel;
        [SerializeField] private GameObject invalidUsernameText;
        [SerializeField] private GameObject duplicateErrorText;
        [SerializeField] private GameObject unknownErrorText;
        [SerializeField] private TextMeshProUGUI nickname;

        #region DATA

        public string gameVersion;
        // public EconomyModel serverEconomy;

        private void Start()
        {
            ServerLogin();
        }

        #endregion

        private void Awake()
        {
            onServerLogin += LoadServerData;
        }

        private void OnDestroy()
        {
            onServerLogin -= LoadServerData;
        }

        #region LOGIN

        private void ServerLogin()
        {
            playFabManagerSo.Login(OnLogginSuccess, OnLogginFailed);
        }

        private void OnLogginSuccess(LoginResult loginResult)
        {
            Debug.Log("User login: " + loginResult.PlayFabId);
            Debug.Log("User newly created: " + loginResult.NewlyCreated);
            Debug.Log("User Name: " + loginResult.InfoResultPayload.PlayerProfile.DisplayName);

            if (loginResult.NewlyCreated || String.IsNullOrWhiteSpace(loginResult.InfoResultPayload.PlayerProfile.DisplayName))
            {
                createAccountPanel.SetActive(true);
            }
            else
            {
                onServerLogin?.Invoke();
            }
        }

        public void CreateAccount()
        {
            invalidUsernameText.SetActive(false);
            duplicateErrorText.SetActive(false);
            unknownErrorText.SetActive(true);
            if (!CheckNickname(nickname.text)) return;
            
            playFabManagerSo.CreateAccount(nickname.text,() =>
                {
                    onServerLogin?.Invoke();
                    createAccountPanel.SetActive(false);
                    playFabManagerSo.Nickname = nickname.text;

                },
                (error) =>
                {
                    Debug.LogError("Create Account Failed!"); 
                    CreateAccountFailed(error==PlayFabErrorCode.DuplicateUsername);

                });
        }

        private void SetNickname()
        {
            
        }

        private bool CheckNickname(string value)
        {
            if (value.Length < 3 || value.Length > 25)
            {
                invalidUsernameText.SetActive(true);
                return false;
            }
            return true;
        }

        private void CreateAccountFailed(bool unkown)
        {
            if (unkown)
                unknownErrorText.SetActive(true);
            else
                duplicateErrorText.SetActive(true);
        }

        private void OnLogginFailed(PlayFabError error)
        {
            Debug.LogError("Login failed: " + error.ErrorMessage);
        }

        #endregion


        #region LOAD_SERVER_DATA

        public void LoadServerData()
        {
            playFabManagerSo.GetTitleData(
                titleData => { LoadGameSetup(titleData.Data); },
                error => { Debug.LogError("Get title data failed: " + error.ErrorMessage); });
        }

        private void LoadGameSetup(Dictionary<string, string> data)
        {
            SetPlayFabVersion(data["ClientVersion"]);
            SetPlayFabEconomyModel(data["EconomySetup"]);
        }

        private void SetPlayFabVersion(string version) => gameVersion = version;

        private void SetPlayFabEconomyModel(string economyJson)
        {
            // JsonUtility.FromJsonOverwrite(economyJson, serverEconomy);
        }

        #endregion
    }

    internal class PlayFabManager
    {
    }
}