using System;
using PlayFab;
using PlayFab.ClientModels;
using SnakeMaze.PlayFab;
using SnakeMaze.Utils;
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
                FunctionName = "GetLoginData"
            };

            PlayFabClientAPI.ExecuteCloudScript<LoginDataResult>(request,
                result =>
                {
                    LoginDataResult serverResponse = (LoginDataResult)result.FunctionResult;

                    if (result.Logs.Count > 0)
                    {
                        if (result.Logs[0].Level == Constants.ErrorMessage)
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
                FunctionName = nameof(CreateAccount),
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<ErrorData>(request,
                result =>
                {
                    ErrorData serverResponse = (ErrorData)result.FunctionResult;

                    for (int i = 0; i < result.Logs.Count; i++)
                    {
                        Debug.Log(result.Logs[i].Message);
                    }

                    if (result.Logs[0].Level == Constants.ErrorMessage)
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

        [ContextMenu("Test Score")]
        public void UpdateUserScore()
        {
            UpdateScore(50);
        }

        public void UpdateScore(int newHighScore)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = nameof(UpdateScore),
                FunctionParameter = new { highScore = newHighScore },
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<BaseServerResult>(request,
                result =>
                {
                    if (result.Logs[0].Level == Constants.ErrorMessage)
                    {
                        Debug.Log(result.Logs[0].Message);
                        Debug.Log("Error updating score");
                    }
                },
                error =>
                {
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.Error);
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.ErrorMessage);
                });
        }
        [ContextMenu("Test Gold")]
        public void UpdateUserGold()
        {
            AddSCCurrency(50, (x) =>{} );
        }
        public void AddSCCurrency(int newGold, Action<int> onSuccsess)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = nameof(AddSCCurrency),
                FunctionParameter = new { amount = newGold },
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<IntTestPlayFab>(request,
                result =>
                {
                    IntTestPlayFab serverResponse = (IntTestPlayFab) result.FunctionResult;
                    if (result.Logs[0].Level == Constants.ErrorMessage)
                    {
                        Debug.Log(result.Logs[0].Message);
                        Debug.Log("Error updating gold");
                    }
                    else
                    {
                        onSuccsess(serverResponse.balance);
                        Debug.Log("PlayFab Total Gold: " + serverResponse.balance);
                    }
                },
                error =>
                {
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.Error);
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.ErrorMessage);
                });
        }

        #endregion

        #region PURCHASES

        public void PurchaseItem(string item, int gold , string moneyType )
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = nameof(PurchaseItem),
                FunctionParameter = new { id = item, price = gold, currency = moneyType },
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<BaseServerResult>(request,
                result =>
                {
                    BaseServerResult serverResponse = (BaseServerResult) result.FunctionResult;
                    if (result.Logs[0].Level == Constants.ErrorMessage)
                    {
                        Debug.Log(result.Logs[0].Message);
                        Debug.Log("Error updating score");
                    }
                    else
                    {
                        
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