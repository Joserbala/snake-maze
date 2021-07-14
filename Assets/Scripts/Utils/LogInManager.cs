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
        [SerializeField] private TextMeshProUGUI nickName;
        
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

            if (loginResult.NewlyCreated || playFabManagerSo.NickName==String.Empty)
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
            if(nickName.text==String.Empty) return;

            playFabManagerSo.NickName = nickName.text;
            playFabManagerSo.CreateAccount(()=>onServerLogin?.Invoke(),()=>Debug.LogError("Create Account Failed!"));
        }
        // private void 
        private void OnLogginFailed(PlayFabError error)
        {
            Debug.LogError("Login failed: " + error.ErrorMessage);
        }

        #endregion

       

        #region LOAD_SERVER_DATA

        public void LoadServerData()
        {
            playFabManagerSo.GetTitleData(
                titleData =>
                {
                    LoadGameSetup(titleData.Data);
                },
                error =>
                {
                    Debug.LogError("Get title data failed: "+ error.ErrorMessage);
                });
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
