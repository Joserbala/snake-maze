using SnakeMaze.Enums;
using UnityEngine;

namespace SnakeMaze.TileMaps
{
    public class WallTile
    {
        private Vector2Int _position;
        private WallSprites _spriteType;

        public Vector2Int Position
        {
            get => _position;
            set => _position = value;
        }

        public WallSprites SpriteType
        {
            get => _spriteType;
            set => _spriteType = value;
        }
    }
}
