using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using SnakeMaze.Enums;
using SnakeMaze.Exceptions;
using SnakeMaze.SO;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.User;
using SnakeMaze.Utils;
using UnityEngine;
using UnityEngine.UI;
using Currency = SnakeMaze.Enums.Currency;

namespace SnakeMaze.UI
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private PlayFabManagerSO playFabManagerSo;
        [SerializeField] private UserInventorySO inventorySo;
        [SerializeField] private Button buyButton;
        [SerializeField] private Currency currencyType;
        [SerializeField] private AbstractSkinItemSO item;

        public void BuySkin()
        {
            var priceData = currencyType switch
            {
                Currency.HC => item.ItemPriceData.HardCoinsPriceData,
                Currency.SC => item.ItemPriceData.SoftCoinsPriceData,
                _=> throw new NotEnumTypeSupportedException()
            };
            if (!item.ItemPriceData.CanBeBoughtWithHc && currencyType == Currency.HC ||
                !item.ItemPriceData.CanBeBoughtWithSc && currencyType == Currency.SC)
            {
                Debug.Log($"{item.ItemId} can not be bought with {currencyType}");
                return;
            }
            playFabManagerSo.PurchaseItem(
                item.ItemId,
                priceData.Price,
                CurrencyUtils.CurrencyToString(priceData.CurrencyType),
                data => OnPurchaseSuccess(data),
                error => OnPurchaseFail(error));
        }

        private void OnPurchaseSuccess(List<ItemInstance> data)
        {
            item.Available = true;
            inventorySo.AddSkinToDictionary(data);
        }

        private void OnPurchaseFail(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
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