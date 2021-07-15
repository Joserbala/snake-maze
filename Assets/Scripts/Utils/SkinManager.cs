using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.Utils
{
    public class SkinManager : MonoBehaviour
    {
        [SerializeField] private SkinContainerSO skinContainerSo;

        [ContextMenu("Change To Default Skin ")]
        public void ChangeToDefaultSkin()
        {
            skinContainerSo.ChangeMazeSkin(MazeSkins.Default);
        }
        [ContextMenu("Change To Mockup Skin ")]
        public void ChangeToMockuptSkin()
        {
            skinContainerSo.ChangeMazeSkin(MazeSkins.Mockup);
        }
        
        
    }
}
