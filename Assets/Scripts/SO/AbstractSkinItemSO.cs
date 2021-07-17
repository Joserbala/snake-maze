using UnityEngine;

namespace SnakeMaze.SO
{
    public abstract class AbstractSkinItemSO : ScriptableObject
    {
        // public abstract T Item { get; }
        public abstract string ItemId { get; }
        
        public abstract int Price { get; }
        
        public abstract string Currency { get; }
        
        public abstract bool Available { get; set; }
    }
}
