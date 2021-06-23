using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SnakeMaze.TileMaps
{
    public class TileMapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap wallTilemap;
        [SerializeField] private MazeSkinSO mazeSkin;


        public void PaintWallTiles(IEnumerable<WallTile> wallTiles)
        {
            foreach (var wallTile in wallTiles)
            {
                var tile = mazeSkin.TileDic[wallTile.SpriteType];
                PaintSingleTile( wallTilemap, tile, wallTile.Position);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}