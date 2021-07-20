using SnakeMaze.SO.UserDataSO;
using TMPro;
using UnityEngine;

namespace SnakeMaze.UI
{
    public class NameAndCoins : MonoBehaviour
    {
        [SerializeField] private UserDataControllerSO userDataControllerSo;
        [SerializeField] private TextMeshProUGUI displayName;
        [SerializeField] private TextMeshProUGUI softCoins;
        [SerializeField] private TextMeshProUGUI hardCoins;

        void Start()
        {
            displayName.text = userDataControllerSo.NickName;
            softCoins.text = "x" + userDataControllerSo.SoftCoins.ToString();
            hardCoins.text = "x" + userDataControllerSo.HardCoins.ToString();
        }
    }
}
