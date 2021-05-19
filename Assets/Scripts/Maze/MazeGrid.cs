using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Maze;
using UnityEngine;

public class MazeGrid 
{
   
   
   private Vector2Int _gridSize;
   private Vector2 _cellSize;
   private MazeCell[,] _grid;
   private int _rows, _columns;
   
   public MazeCell[,] Grid
   {
      get => _grid;
      set => _grid = value;
   }

   public int Rows => _rows;
   public int Columns => _columns;

   public MazeGrid(Vector2Int gridSize, Vector2 cellSize)
   {
      _gridSize = gridSize;
      _cellSize = cellSize;

      _rows = (int) Mathf.Ceil(_gridSize.x / _cellSize.x);
      _columns = (int) Mathf.Ceil(_gridSize.y / _cellSize.y);

      _grid = new MazeCell[_rows, _columns];
   }
}
