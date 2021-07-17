using SnakeMaze.PlayFab;

namespace SnakeMaze.User
{
    public class ScoreData
    {
        public int Score { get; set; }
    }

    public class EconomyData
    {
        public int SoftCoin{get;set;}
        public int HardCoin{get;set;}

        public void SetEconomyData(CurrencyData data)
        {
            SoftCoin = data.SoftCoins;
            HardCoin = data.HardCoins;
        }
    }
}
