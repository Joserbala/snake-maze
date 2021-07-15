using System;
using PlayFab;
using PlayFab.ClientModels;
using SnakeMaze.PlayFab;
using UnityEngine;

namespace SnakeMaze.SO.PlayFabManager
{
    [CreateAssetMenu(fileName = "PlayFabManager", menuName = "Scriptables/PlayFab/PlayFabManager")]
    public class PlayFabManagerSO : InitiableSO
    {
        [SerializeField] private bool isTest = true;
        private string displayName;

        public static string PLAYFAB_TITLE_ID_TEST = "E0CD8";
        public static string PLAYFAB_TITLE_RELEASE = "6F99B";

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public override void InitScriptable()
        {
            SetupPlayFabServer();
        }

        #region LOGIN

        private void SetupPlayFabServer()
        {
            PlayFabSettings.TitleId = isTest ? PLAYFAB_TITLE_ID_TEST : PLAYFAB_TITLE_RELEASE;
        }

        public void Login(Action<LoginResult> onSuccess, Action<PlayFabError> onFail)
        {
            var request = new LoginWithCustomIDRequest()
            {
                CreateAccount = true,
                CustomId = SystemInfo.deviceUniqueIdentifier,

                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };

            PlayFabClientAPI.LoginWithCustomID(request, onSuccess, onFail);
        }

        public void GetLoginData(Action<LoginDataResult> onSuccess, Action onFail)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "GetLoginData",
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<LoginDataResult>(request,
                result =>
                {
                    LoginDataResult serverResponse = (LoginDataResult) result.FunctionResult;

                    if (result.Logs.Count > 0)
                    {
                        if (result.Logs[0].Level == "Error")
                        {
                            onFail();
                        }

                        else
                        {
                            onSuccess(serverResponse);
                        }
                    }
                    else
                    {
                        onSuccess(serverResponse);
                    }
                },
                error => { });
        }

        #endregion

        #region TITLE_DATA

        public void GetTitleData(Action<GetTitleDataResult> onSuccess, Action<PlayFabError> onError)
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), onSuccess, onError);
        }

        #endregion

        #region CREATE_ACCOUNT

        public void CreateAccount(string nickname, Action onSuccess, Action onFail)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "CreateAccount",
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<ErrorData>(request,
                result =>
                {
                    ErrorData serverResponse = (ErrorData) result.FunctionResult;

                    for (int i = 0; i < result.Logs.Count; i++)
                    {
                        Debug.Log(result.Logs[i].Message);
                    }

                    if (result.Logs[0].Level == "Error")
                    {
                        onFail();
                    }

                    else
                        onSuccess();
                },
                error =>
                {
                    onFail();
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.Error);
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.ErrorMessage);
                });
        }

        #endregion

        #region USER_DATA

        [ContextMenu("Testt")]
        public void UpdateUserScore()
        {
            UpdateUserScore(20);
        }

        public void UpdateUserScore(int newHighScore)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "UpdateScore",
                GeneratePlayStreamEvent = true,
                FunctionParameter = new {highScore = newHighScore}
            };
            PlayFabClientAPI.ExecuteCloudScript<ErrorDataFull>(request,
                result =>
                {
                    Debug.Log("Score Updated Successfully");
                    if (result.Logs[0].Level == "Error")
                    {
                        Debug.Log("Error updating score");
                    }
                },
                error =>
                {
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.Error);
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.ErrorMessage);
                });
        }

        #endregion
    }
}