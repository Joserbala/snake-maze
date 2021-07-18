using System;
using System.Collections.Generic;
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
                FunctionName = nameof(GetLoginData)
            };

            PlayFabClientAPI.ExecuteCloudScript<LoginDataResult>(
                request,
                result =>
                {
                    LoginDataResult serverResponse = (LoginDataResult) result.FunctionResult;
                    
                    if(!serverResponse.isSuccess)
                    {
                        onFail();
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

            PlayFabClientAPI.ExecuteCloudScript<ErrorData>(
                request,
                result =>
                {
                    BaseServerResult serverResponse = (BaseServerResult) result.FunctionResult;
                    
                    if(!serverResponse.isSuccess)
                    {
                        onFail();
                    }
                    else
                    {
                        onSuccess();
                    }
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
                FunctionParameter = new {highScore = newHighScore},
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<BaseServerResult>(
                request,
                result =>
                {
                    BaseServerResult serverResponse = (BaseServerResult) result.FunctionResult;
                    if(!serverResponse.isSuccess)
                        Debug.Log("Error updating score: " + serverResponse.error);
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
            AddSCCurrency(50, (x) => { });
        }

        public void AddSCCurrency(int newGold, Action<int> onSuccsess)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = nameof(AddSCCurrency),
                FunctionParameter = new {amount = newGold},
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript<IntTestPlayFab>(
                request,
                result =>
                {
                    IntTestPlayFab serverResponse = (IntTestPlayFab) result.FunctionResult;
                    if(!serverResponse.isSuccess)
                    {
                        Debug.Log("Error updating gold: " + serverResponse.error);
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

        public void UpdateUserInventoryItemCustomData(ItemInstance item, Action onSuccess)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = nameof(UpdateUserInventoryItemCustomData),
                FunctionParameter = new {itemInstanceId = item.ItemInstanceId, itemId = item.ItemId},
                GeneratePlayStreamEvent = true
            };
            PlayFabClientAPI.ExecuteCloudScript<BaseServerResult>(
                request,
                result =>
                {
                    BaseServerResult serverResponse = (BaseServerResult) result.FunctionResult;
                    if (serverResponse.isSuccess)
                    {
                        Debug.Log($"CustomData of {item.ItemId} updated successfully ");
                        onSuccess();
                    }

                    else
                    {
                        Debug.Log($"Error updating CustomData of {item.ItemId} ");
                        Debug.Log("Error: " + serverResponse.error);
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

        [ContextMenu("TestPurchase")]
        public void PurchaseDefaultSkins()
        {
            PurchaseItem(Constants.DefaultMazeSkin, 0, "SC", instances =>
                {
                    Debug.Log("Success!");
                    PurchaseItem(Constants.DefaultSnakeSkin, 0, "SC", instances => { Debug.Log("Success!"); },
                        error => { Debug.LogError(error.GenerateErrorReport()); });
                },
                error => { Debug.LogError(error.GenerateErrorReport()); });
        }

        public void PurchaseItem(string item, int gold, string moneyType, Action<List<ItemInstance>> onSuccess,
            Action<PlayFabError> onFail)
        {
            var request = new PurchaseItemRequest()
            {
                ItemId = item,
                Price = gold,
                VirtualCurrency = moneyType
            };

            PlayFabClientAPI.PurchaseItem(
                request,
                result =>
                {
                    foreach (var item in result.Items)
                    {
                        UpdateUserInventoryItemCustomData(item, ()=>onSuccess(result.Items));
                    }
                },
                error => onFail(error)
            );
        }

        #endregion
    }
}