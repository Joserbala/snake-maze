using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.TileMaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "MazeSkin", menuName = "Scriptables/TileMaps/MazeSkinSO")]
    public class MazeSkinSO : ScriptableObject
    {
        [SerializeField] private TileBase 
            topRightLeft,
            topRightBot,
            topLeftBot,
            topRight,
            topLeft,
            topBot,
            top,
            rightLeftBot,
            rightLeft,
            rightBot,
            leftBot,
            right,
            left,
            bot;

        private Dictionary<WallSprites, TileBase> _tileDic=new Dictionary<WallSprites, TileBase>();

        public Dictionary<WallSprites, TileBase> TileDic
        {
            get => _tileDic;
            set => _tileDic = value;
        }

        public void InitMazeSkin()
        {
           _tileDic.Add(WallSprites.TopRightLeft,topRightLeft);
           _tileDic.Add(WallSprites.TopRightBot,topRightBot);
           _tileDic.Add(WallSprites.TopLeftBot,topLeftBot);
           _tileDic.Add(WallSprites.TopRight,topRight);
           _tileDic.Add(WallSprites.TopLeft,topLeft);
           _tileDic.Add(WallSprites.TopBot,topBot);
           _tileDic.Add(WallSprites.Top,top);
           _tileDic.Add(WallSprites.RightLeftBot,rightLeftBot);
           _tileDic.Add(WallSprites.RightLeft,rightLeft);
           _tileDic.Add(WallSprites.RightBot,rightBot);
           _tileDic.Add(WallSprites.LeftBot,leftBot);
           _tileDic.Add(WallSprites.Right,right);
           _tileDic.Add(WallSprites.Left,left);
           _tileDic.Add(WallSprites.Bot,bot);

        }

    }
}