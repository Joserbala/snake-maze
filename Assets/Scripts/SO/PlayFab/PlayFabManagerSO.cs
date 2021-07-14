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
        private string _nickName;

        public static string PLAYFAB_TITLE_ID_TEST = "E0CD8";
        public static string PLAYFAB_TITLE_RELEASE = "6F99B";

        public string NickName
        {
            get => _nickName;
            set => _nickName = value;
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
                CustomId = SystemInfo.deviceUniqueIdentifier
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
                    LoginDataResult serverResponse = result.FunctionResult as LoginDataResult;
                    if (serverResponse.Result == 200)
                    {
                        onSuccess(serverResponse);
                    }
                    else
                    {
                        onFail();
                    }
                },
                error =>
                {
                });
        }
        #endregion

        #region TITLE_DATA

        public void GetTitleData(Action<GetTitleDataResult> onSuccess, Action<PlayFabError> onError)
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), onSuccess, onError);
        }

        #endregion

        #region CREATE_ACCOUNT

        public void CreateAccount(Action onSuccess, Action onFail)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "CreateAccount",
                GeneratePlayStreamEvent = true
            };
            PlayFabClientAPI.ExecuteCloudScript<CloudScriptResult>(request,
                result =>
                {
                    CloudScriptResult serverResponse = (CloudScriptResult)
                        result.FunctionResult; //as CloudScriptResult;
                    Debug.Log("Create account response: " + serverResponse.Result);

                    if (serverResponse.Result == 200)
                    {
                        onSuccess();
                    }
                    else
                    {
                        onFail();
                    }
                },
                error =>
                {
                    onFail();
                    Debug.LogError("CUIDADO FUNCTION FAILED: " + error.ErrorMessage);
                });
        }

        #endregion
    }
}
