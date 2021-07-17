using PlayFab.ClientModels;
using UnityEngine;

namespace SnakeMaze.SO.Items
{
    
    [CreateAssetMenu(fileName = "MazeSkinItem", menuName = "Scriptables/MazeSkinItemSO")]
    public class MazeSkinItemSO : AbstractSkinItemSO
    {
        [SerializeField] private MazeSkinSO currentSkin;
        [SerializeField] private string itemId;
        [SerializeField] private int price;
        [SerializeField] private Currency currency;


        public  MazeSkinSO Item => currentSkin;
        
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
