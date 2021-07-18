using System.Collections.Generic;
using PlayFab.ClientModels;
using SnakeMaze.Enums;
using UnityEngine;
using Currency = SnakeMaze.Enums.Currency;

namespace SnakeMaze.SO.Items
{
    [CreateAssetMenu(fileName = "Catalog", menuName = "Scriptables/CatalogSO")]
    public class CatalogSO : ScriptableObject
    {
        [SerializeField] private List<AbstractSkinItemSO> catalogList;

        public void InitCatalog(List<CatalogItem> serverCatalog)
        {
            var dictionary = new Dictionary<string, CatalogItem>();
            foreach (var item in serverCatalog)
            {
                dictionary.Add(item.ItemId, item);
            }

            foreach (var item in catalogList)
            {
                var catalogItem = dictionary[item.ItemId];
                item.SetPriceAndCurrency(
                    (int) catalogItem.VirtualCurrencyPrices[CurrencyUtils.CurrencyToString(Currency.HC)], Currency.HC);
                item.SetPriceAndCurrency(
                    (int) catalogItem.VirtualCurrencyPrices[CurrencyUtils.CurrencyToString(Currency.SC)], Currency.SC);
            }
        }
    }
}