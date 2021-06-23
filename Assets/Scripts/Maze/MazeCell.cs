using System;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.TileMaps;
using UnityEngine;

namespace SnakeMaze.Maze
{
    

    public class MazeCell
    {
        public int GridX { get; }
        public int GridY { get; }
        public Vector3 Position { get; }
        public GameObject CellPrefab { get; }
        public WallTile Tile { get; private set; }
        public Dictionary<Directions, GameObject> Walls { get; set; }
        public bool InMaze = false;
        public bool IsFrontier = false;
        private char[] spriteBinaryType = new []{'1','1','1','1'};

        public MazeCell(GameObject cellPrefab, Vector3 pos, int i, int j)
        {
            CellPrefab = cellPrefab;
            Position = pos;
            GridX = i;
            GridY = j;

            Walls = new Dictionary<Directions, GameObject>();

            for (int k = 0; k < CellPrefab.transform.childCount; k++)
            {
                var index = (k / 2 + 1) * Mathf.Pow(-1,k);
                Walls.Add((Directions)index,CellPrefab.transform.GetChild(k).gameObject);
            }
        }

        // public void GetWall(Directions dir)
        // {
        //     var index = dir switch
        //     {
        //         Directions.Up => 0,
        //         Directions.Down => 2,
        //         Directions.Right => 1,
        //         Directions.Left => 3,
        //     };
        //     spriteBinaryType[index] = '0';
        // }

        // public void SetWallTile()
        // {
        //     var binary = spriteBinaryType.ToString();
        //     var number = Convert.ToInt32(binary, 2);
        //     Tile.SpriteType = (WallSprites) number;
        //     Vector2Int pos = new Vector2Int((int)Position.x, (int)Position.y);
        //     Tile.Position = pos;
        // }
        public GameObject GetWall(Directions dir)
        {
            GameObject wall;
            Walls.TryGetValue(dir,out wall);
            return wall;
        }
    }
}