using UnityEngine;

namespace SnakeMaze.Utils
{
    public static class EconomyManager
    {
        private static  float _pointToCoinRatio = 100f;
        private static  float _coinLooseRatioOnDeath=2f;

        public static int SetCoinsFromPoint(bool hasWon, int points)
        {
            var ratio = hasWon ? _coinLooseRatioOnDeath * _pointToCoinRatio :  _pointToCoinRatio;
            var coins = (int) Mathf.Floor(points/ratio);
            return coins;
        }

        public static void SetRatios(int pointToCoinRatio, int coinLooseRatioOnDeath)
        {
            _pointToCoinRatio = pointToCoinRatio;
            _coinLooseRatioOnDeath = coinLooseRatioOnDeath;
        }
    }
}
