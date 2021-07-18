using SnakeMaze.Enums;
using SnakeMaze.Utils;
using UnityEngine;

namespace SnakeMaze.SO.Items
{

    [CreateAssetMenu(fileName = "SnakeSkinItem", menuName = "Scriptables/SnakeSkinItemSO")]
    public class SnakeSkinItemSO : AbstractSkinItemSO
    {
        [SerializeField] private SnakeSkinSO currentSkin;
        [SerializeField] private string itemId;


        public SnakeSkinSO Item => currentSkin;

        private bool _available;

        public override string ItemId => itemId;
        public override FullPriceData ItemPriceData { get; set; }
        

    public override bool Available
        {
            get => _available;
            set => _available = value;
        }

        

    }
}
