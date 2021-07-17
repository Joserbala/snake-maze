using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

namespace SnakeMaze.PlayFab
{
    public class BaseServerResult
    {
        public bool isSuccess;
        public string error;
    }

    [Serializable]
    public class LoginDataResult : BaseServerResult
    {
        public LoginDataStructure loginData;
    }

    public class LoginDataStructure : BaseServerResult
    {
        public Dictionary<string, UserDataRecord> readOnlyData;

        // public List<ItemInstance> inventory;
        public CurrencyData currency;
    }

    public class IntTestPlayFab : BaseServerResult
    {
        public int balance;
    }

    public class CurrencyData : BaseServerResult
    {
        public int softCoins;
        public int hardCoins;
    }

    public class SkinData : BaseServerResult
    {
        public string itemClass;
        public string itemId;
    }
    public class ErrorData
    {
        public PlayFabErrorCode errorCode;
    }

    public class ErrorDataFull
    {
        public PlayFabError error;
    }

    public class ErrorDataTest : ErrorData
    {
        public String nickname;
    }
}