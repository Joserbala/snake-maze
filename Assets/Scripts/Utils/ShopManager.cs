using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using SnakeMaze.SO;
using SnakeMaze.UI;
using SnakeMaze.User;
using UnityEngine;

namespace SnakeMaze.Utils
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private List<BuyButton> buyButtonList;
        [SerializeField] private List<SelectButton> selectButtonList;
        [SerializeField] private BusBuySkinSO buySkinSo;

        private void Awake()
        {
            InitButtons();
        }

        private void InitButtons()
        {
            foreach (var buyButton in buyButtonList)
            {
                if(!buyButton.Item.Available)
                    buyButton.gameObject.SetActive(true);
            }
            foreach (var selectButton in selectButtonList)
            {
                if(selectButton.Item.Available)
                    selectButton.gameObject.SetActive(true);
            }
            
        }

        private void SetSelectButtonActive(string itemId)
        {
            foreach (var selectButton in selectButtonList)
            {
                if (selectButton.Item.ItemId == itemId)
                    selectButton.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            buySkinSo.OnBuySkin += SetSelectButtonActive;
        }

        private void OnDisable()
        {
            buySkinSo.OnBuySkin -= SetSelectButtonActive;
        }
    }
}
