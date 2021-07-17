using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

namespace SnakeMaze.PlayFab
{

    [Serializable]
    public class LoginDataResult 
    {
        public LoginDataStructure LoginData;
    }

    public class LoginDataStructure
    {
        public Dictionary<string, UserDataRecord> ReadOnlyData;
        // public List<ItemInstance> Inventory;
        public CurrencyData Currency;
    }

    public class IntTestPlayFab
    {
        public int Balance;
    }

    public class CurrencyData
    {
        public int SoftCoins;
        public int HardCoins;
    }

    public class ErrorData 
    {
        public PlayFabErrorCode ErrorCode;
    }
    public class ErrorDataFull 
    {
        public PlayFabError Error;
    }
    public class ErrorDataTest : ErrorData
    {
        public String Nickname;
    }
}