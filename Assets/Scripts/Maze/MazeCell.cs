using System.Collections.Generic;
using SnakeMaze.Enums;
using UnityEngine;

namespace SnakeMaze.Maze
{
    

    public class MazeCell
    {
        public int GridX { get; }
        public int GridY { get; }
        public Vector3 Position { get; }
        public GameObject CellPrefab { get; }
        public Dictionary<Directions, GameObject> Walls { get; set; }
        public bool InMaze = false;
        public bool IsFrontier = false;

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

        public GameObject GetWall(Directions dir)
        {
            GameObject wall;
            Walls.TryGetValue(dir,out wall);
            return wall;
        }
    }
}