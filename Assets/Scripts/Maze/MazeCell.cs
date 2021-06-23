using System;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.Exceptions;
using SnakeMaze.TileMaps;
using UnityEngine;

namespace SnakeMaze.Maze
{
    

    public class MazeCell
    {
        public int GridX { get; }
        public int GridY { get; }
        public Vector3 Position { get; }
        public WallTile Tile { get; private set; }
        public Dictionary<Directions, GameObject> Walls { get; set; }
        public bool InMaze = false;
        public bool IsFrontier = false;
        private char[] spriteBinaryType;
        
        public MazeCell(Vector3 pos, int i, int j)
        {
            Position = pos;
            GridX = i;
            GridY = j;
            spriteBinaryType= new []{'1','1','1','1'};
        }

        public void GetWall(Directions dir)
        {
            var index = dir switch
            {
                Directions.Up => 0,
                Directions.Down => 2,
                Directions.Right => 1,
                Directions.Left => 3,
                _=> throw new NotEnumTypeSupportedException()
            };
            spriteBinaryType[index] = '0';
        }

        public void SetWallTile()
        {
            string binary = "" ;
            for (int i = 0; i < spriteBinaryType.Length; i++)
            {
                binary += spriteBinaryType[i];
            }
            var number = Convert.ToInt32(binary, 2);
            Vector2Int pos = new Vector2Int((int)Position.x, (int)Position.y);
            Tile = new WallTile(pos, (WallSprites) number);
        }
    }
}