using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using SnakeMaze.Enums;
using SnakeMaze.Exceptions;
using SnakeMaze.SO;
using SnakeMaze.SO.PlayFab;
using SnakeMaze.SO.PlayFabManager;
using SnakeMaze.User;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Currency = SnakeMaze.Enums.Currency;

namespace SnakeMaze.UI
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private PlayFabManagerSO playFabManagerSo;
        [SerializeField] private BusServerCallSO busServerCallSo;
        [SerializeField] private UserInventorySO inventorySo;
        [SerializeField] private Currency currencyType;
        [SerializeField] private AbstractSkinItemSO item;
        [SerializeField] private BusBuySkinSO buySkinSo;
        [SerializeField] private TextMeshProUGUI price;
        [Header("On buy error panel")]
        [SerializeField] private GameObject panelError;
        [SerializeField] private float timeToDissappearPanel = 1.0f;

        private Button _buyButton;

        public AbstractSkinItemSO Item
        {
            get => item;
            set => item = value;
        }

        private void Awake()
        {
            _buyButton = GetComponent<Button>();
        }

        private void Start()
        {
            SetPrice();
        }

        private void SetPrice()
        {
            price.text = currencyType switch
            {
                Currency.SC => item.ItemPriceData.SoftCoinsPriceData.Price.ToString(),
                Currency.HC => item.ItemPriceData.HardCoinsPriceData.Price.ToString(),
                _ => throw new NotEnumTypeSupportedException()
            };
        }

        public void BuySkin()
        {
            var priceData = currencyType switch
            {
                Currency.HC => item.ItemPriceData.HardCoinsPriceData,
                Currency.SC => item.ItemPriceData.SoftCoinsPriceData,
                _ => throw new NotEnumTypeSupportedException()
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
                data =>
                {
                    OnPurchaseSuccess(data);
                },
                error => OnPurchaseFail(error));
        }

        private void OnPurchaseSuccess(List<ItemInstance> data)
        {
            item.Available = true;
            inventorySo.AddSkinToDictionary(data);
            busServerCallSo.OnServerResponse?.Invoke();
            buySkinSo.OnBuySkin?.Invoke(item.ItemId);
        }

        private void OnPurchaseFail(PlayFabError error)
        {
            panelError.SetActive(true);
            StartCoroutine(DeactivateErrorPanelYield());
            Debug.LogError(error.GenerateErrorReport());
            busServerCallSo.OnServerResponse?.Invoke();
        }

        private IEnumerator DeactivateErrorPanelYield() // TODO: Do tween.
        {
            yield return new WaitForSeconds(timeToDissappearPanel);
            panelError.SetActive(false);
        }

        private void CheckButtonState(string itemId)
        {
            gameObject.SetActive(itemId != item.ItemId);
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(BuySkin);
            buySkinSo.OnBuySkin += CheckButtonState;
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(BuySkin);
            buySkinSo.OnBuySkin -= CheckButtonState;
        }
    }
}