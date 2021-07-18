using System;
using SnakeMaze.Enums;

namespace SnakeMaze.Utils
{
    [Serializable]
    public class FullPriceData 
    {
        private PriceData _softCoinsPriceData = new PriceData(Currency.SC);
        private PriceData _hardCoinsPriceData= new PriceData(Currency.HC);

        public PriceData SoftCoinsPriceData
        {
            get => _softCoinsPriceData;
            set => _softCoinsPriceData = value;
        }

        public PriceData HardCoinsPriceData
        {
            get => _hardCoinsPriceData;
            set => _hardCoinsPriceData = value;
        }
    }
    [Serializable]
    public class PriceData
    {
        public int Price;
        public Currency CurrencyType;

        public PriceData(Currency currency)
        {
            CurrencyType = currency;
        }
    }
}
