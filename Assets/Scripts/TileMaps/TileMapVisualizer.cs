using System;
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
        [SerializeField] private Tilemap foodTilemap;
        [SerializeField] private MazeSkinSO mazeSkin;


        private void Awake()
        {
            mazeSkin.InitMazeSkin();
        }


        public void PaintWallTiles(IEnumerable<WallTile> wallTiles)
        {
            foreach (var wallTile in wallTiles)
            {
                var tile = mazeSkin.TileDic[wallTile.SpriteType];
                PaintSingleTile(wallTilemap, tile, wallTile.Position);
            }
        }

        public void PaintCorridorTiles(Vector2Int position, Directions direction, int amount, bool horizontalCorridor)
        {
            for (int i = 0; i < amount; i++)
            {
                var tile = horizontalCorridor ? mazeSkin.HorizontalCorridor : mazeSkin.VerticalCorridor;
                var dir = DirectionsActions.DirectionsToVector2(direction);
                var actualDir = new Vector2Int((int) dir.x, (int) dir.y);
                PaintSingleTile(wallTilemap, tile, position + actualDir * i);
            }
        }


        public void PaintExitTile(Vector2Int position)
        {
            PaintSingleTile(wallTilemap, mazeSkin.Exit, position);
        }

        public void PaintFoodTile(Vector2Int position)
        {
            Debug.Log("Hey");
            PaintSingleTile(foodTilemap, mazeSkin.Food, position);
        }
        public void EraseFoodTile(Vector2Int position)
        {
            PaintSingleTile(foodTilemap, null, position);
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int) position);
            tilemap.SetTile(tilePosition, tile);
        }
    }
}