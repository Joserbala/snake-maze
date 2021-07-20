using System;
using SnakeMaze.SO;
using SnakeMaze.User;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    [RequireComponent(typeof(Button))]
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private AbstractSkinItemSO item;
        [SerializeField] private UserInventorySO inventorySo;
        [SerializeField] private SkinContainerSO skinContainerSo;
        [SerializeField] private BusSelectSkinSO busSelectSkinSo;

        private Button _selectButton;

        private void Awake()
        {
            _selectButton = GetComponent<Button>();
        }

        public AbstractSkinItemSO Item
        {
            get => item;
            set => item = value;
        }

        public void SelectItem()
        {
            if (inventorySo.SnakeDictionary.TryGetValue(item.ItemId, out var snakeSkin))
            {
                skinContainerSo.ChangeSnakeSkin(snakeSkin);
                busSelectSkinSo.OnButtonSelect?.Invoke(this);
            }
            else
            {
                if(inventorySo.MazeDictionary.TryGetValue(item.ItemId, out var mazeSkin))
                {
                    skinContainerSo.ChangeMazeSkin(mazeSkin);
                    busSelectSkinSo.OnButtonSelect?.Invoke(this);
                }
            }
        }

        private void CheckButtonState(SelectButton selectButton)
        {
            _selectButton.interactable = selectButton != this;
        }

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(SelectItem);
            busSelectSkinSo.OnButtonSelect += CheckButtonState;
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(SelectItem);
            busSelectSkinSo.OnButtonSelect -= CheckButtonState;
        }
        
    }
}