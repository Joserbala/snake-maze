using System;
using SnakeMaze.UI;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "BusSelectSkin", menuName = "Scriptables/BusSO/BusSelectSkinSO")]
    public class BusSelectSkinSO : ScriptableObject
    {
        public Action<SelectButton> OnButtonSelect;
    }
}
