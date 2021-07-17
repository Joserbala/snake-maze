using SnakeMaze.Enums;
using UnityEngine;

namespace SnakeMaze.SO.Items
{
    
    [CreateAssetMenu(fileName = "SnakeSkinItem", menuName = "Scriptables/SnakeSkinItemSO")]
    public class SnakeSkinItemSO : AbstractSkinItemSO
    {
        [SerializeField] private SnakeSkinSO currentSkin;
        [SerializeField] private string itemId;
        [SerializeField] private int price;
        [SerializeField] private Currency currency;


        public  SnakeSkinSO Item => currentSkin;
        
        private bool _available;

        public override string ItemId => itemId;
        
        public override int Price => price;
        
        public override string Currency => currency.ToString();

        public override bool Available
        {
            get => _available;
            set => _available = value;
        }

        

    }
}
