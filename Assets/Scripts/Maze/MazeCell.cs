using System.Collections.Generic;
using UnityEngine;

namespace SnakeMaze.Maze
{
    public enum Directions
    {
        Up = 1,
        Down = -1,
        Right = 2,
        Left = -2
    }

    public class MazeCell
    {
        public int GridX { get; }
        public int GridY { get; }
        public Vector3 Position { get; }
        public GameObject CellPrefab { get; }
        public Dictionary<Directions, GameObject> Walls { get; set; }

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
    }
}