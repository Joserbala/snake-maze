using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeMaze.Maze
{
    public class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Vector2 cellSize = new Vector2(0.5f, 0.5f);
        [SerializeField] private Vector2 bottomLeft = new Vector2(0, 0);

        private MazeGrid _grid;
        private List<Vector2> _frontier;
        private int _rows, _columns;

        private void Awake()
        {
            cellPrefab.transform.localScale = new Vector3(cellSize.x, cellSize.y, 1);
        }

        private void Start()
        {
            CreateTheGrid();
            RunPrimm();
        }

        private void CreateTheGrid()
        {
            _grid = new MazeGrid(gridSize, cellSize);

            for (int i = 0; i < _grid.Rows; i++)
            for (int j = 0; j < _grid.Columns; j++)
            {
                Vector3 cellPosition = bottomLeft
                                       + Vector2.right * (i * cellSize.x + cellSize.x / 2)
                                       + Vector2.up * (j * cellSize.y + cellSize.y / 2);
                var currentCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
                currentCell.name = "Cell" + i + "," + j;
                _grid.Grid[i, j] = new MazeCell(currentCell, cellPosition, i, j);
            }
        }

        private void RunPrimm()
        {
            _frontier = new List<Vector2>();
            SetCellInMaze(Random.Range(0, _grid.Rows), Random.Range(0, _grid.Columns));
            var neighbors = new List<Vector2>();
            var cellPos = new Vector2();
            var neighborSelected = new Vector2();
            var removePosition = 0;
            var direction = Directions.Right;
            while (_frontier.Count != 0)
            {
                removePosition = Random.Range(0, _frontier.Count);
                cellPos = _frontier[removePosition];
                _frontier.RemoveAt(removePosition);

                neighbors = GetNeighbors((int) cellPos.x, (int) cellPos.y);
                neighborSelected = neighbors[Random.Range(0, neighbors.Count)];

                direction = GetDirection((int) cellPos.x, (int) cellPos.y, (int) neighborSelected.x,
                    (int) neighborSelected.y);
                _grid.Grid[(int) cellPos.x, (int) cellPos.y].GetWall(direction).SetActive(false);
                // direction =(Directions) direction * -1;
                _grid.Grid[(int) neighborSelected.x,
                    (int) neighborSelected.y].GetWall(direction).SetActive(false);
                SetCellInMaze((int) cellPos.x, (int) cellPos.y);
            }
        }

        private List<Vector2> GetNeighbors(int i, int j)
        {
            var neighbors = new List<Vector2>();

            void AddNeighbor(int x, int y)
            {
                neighbors.Add(new Vector2(x, y));
            }

            if (i > 0 && _grid.Grid[i - 1, j].InMaze)
                AddNeighbor(i - 1, j);

            if (i + 1 < _grid.Rows && _grid.Grid[i + 1, j].InMaze)
                AddNeighbor(i + 1, j);

            if (j > 0 && _grid.Grid[i, j - 1].InMaze)
                AddNeighbor(i, j - 1);

            if (j + 1 < _grid.Columns && _grid.Grid[i, j + 1].InMaze)
                AddNeighbor(i, j + 1);
            return neighbors;
        }

        private void AddFrontier(int i, int j)
        {
            if (i < 0 || j < 0 || i >= _grid.Rows || j >= _grid.Columns) return;
            if (_grid.Grid[i, j].IsFrontier || _grid.Grid[i, j].InMaze) return;

            _frontier.Add(new Vector2(i, j));
            _grid.Grid[i, j].IsFrontier = true;
        }

        private void SetCellInMaze(int i, int j)
        {
            _grid.Grid[i, j].InMaze = true;
            _grid.Grid[i, j].IsFrontier = false;

            AddFrontier(i - 1, j);
            AddFrontier(i + 1, j);
            AddFrontier(i, j + 1);
            AddFrontier(i, j - 1);
        }

        private Directions GetDirection(int i, int j, int x, int y)
        {
            var dir = Directions.Right;
            if (i > x)
                dir = Directions.Right;
            if (i < x)
                dir = Directions.Left;
            if (j > y)
                dir = Directions.Up;
            if (j < y)
                dir = Directions.Down;
            return dir;
        }
    }
}