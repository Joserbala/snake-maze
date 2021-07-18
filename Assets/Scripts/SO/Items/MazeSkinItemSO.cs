using System;
using SnakeMaze.Enums;
using SnakeMaze.Utils;
using UnityEngine;

namespace SnakeMaze.SO.Items
{
    
    [CreateAssetMenu(fileName = "MazeSkinItem", menuName = "Scriptables/MazeSkinItemSO")]
    public class MazeSkinItemSO : AbstractSkinItemSO
    {
        [SerializeField] private MazeSkinSO currentSkin;
        [SerializeField] private string itemId;


        public  MazeSkinSO Item => currentSkin;
        
        private bool _available;

        public override string ItemId => itemId;
        public override FullPriceData ItemPriceData { get; set; }
        

        public override bool Available
        {
            get => _available;
            set => _available = value;
        }
        
        public override void HasCurrencyType(Currency currency)
        {
            switch (currency)
            {
                case Currency.HC:
                    ItemPriceData.CanBeBoughtWithHc = true;
                    break;
                case Currency.SC:
                    ItemPriceData.CanBeBoughtWithSc = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }
    }
}
