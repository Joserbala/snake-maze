using PlayFab.ClientModels;
using SnakeMaze.SO;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.User;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private PlayFabManagerSO playFabManagerSo;
        [SerializeField] private UserInventorySO inventorySo;
        [SerializeField] private Button buyButton;
        [SerializeField] private AbstractSkinItemSO item;
        
        public void BuySkin()
        {
            playFabManagerSo.PurchaseItem(item.ItemId, item.Price, item.Currency,
                data=>OnPurchaseSuccess(data),
                OnPurchaseFail);
        }

        private void OnPurchaseSuccess(ItemInstance[] data)
        {
            item.Available = true;
            inventorySo.AddSkinToDictionary(data);
        }

        private void OnPurchaseFail()
        {
            Debug.Log($"Failed at purchasing {item.ItemId}");
        }

        private void OnEnable()
        {
            buyButton.onClick.AddListener(BuySkin);
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveListener(BuySkin);
        }
    }
}
