using System;
using System.Collections.Generic;
using PlayFab.ClientModels;

namespace SnakeMaze.PlayFab
{
    [Serializable]
    public class CloudScriptResult
    {
        //Mismo nombre que la variable que devuelve la función de cloud script.
        public int Result;
    }

    [Serializable]
    public class LoginDataResult : CloudScriptResult
    {
        public LoginDataStructure LoginData;
    }

    public class LoginDataStructure
    {
        public Dictionary<string, UserDataRecord> ReadOnlyData;
        public List<ItemInstance> Inventory;
        public CurrencyData Currency;
    }

    public class CurrencyData
    {
        public int SoftCoins;
        public int HardCoins;
    }

    public class ErrorData : CloudScriptResult
    {
        public int ErrorCode;
    }
}