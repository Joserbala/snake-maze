using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.CameraUtil
{
    public class BackgroundCamera : MonoBehaviour
    {
        [SerializeField] private SkinContainerSO skinContainerSo;

        private void Start()
        {
            if(Camera.main!=null)
             Camera.main.backgroundColor = skinContainerSo.CurrentMazeSkin.BackgroundColor;
        }
    }
}
