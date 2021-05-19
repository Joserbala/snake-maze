using Unity.Mathematics;
using UnityEngine;

namespace SnakeMaze.Maze
{
    public class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Vector2 cellSize = new Vector2(0.5f, 0.5f);
        [SerializeField] private Vector2 bottomLeft = new Vector2(0, 0);

        private MazeGrid _grid;
        private int _rows, _columns;

        private void Awake()
        {
            cellPrefab.transform.localScale = new Vector3(cellSize.x, cellSize.y, 1);
        }
        
        private void Start()
        {
            CreateTheGrid();
        }

        private void CreateTheGrid()
        {
            _grid = new MazeGrid(gridSize,cellSize);

            for (int i = 0; i < _grid.Rows; i++)
            for (int j = 0; j < _grid.Columns; j++)
            {
                Vector3 cellPosition = bottomLeft
                                       + Vector2.right * (i * cellSize.x + cellSize.x / 2)
                                       + Vector2.up * (j * cellSize.y + cellSize.y / 2);
                var currentCell = Instantiate(cellPrefab, cellPosition, quaternion.identity, transform);
                currentCell.name = "Cell" + i + "," + j;
                _grid.Grid[i, j] = new MazeCell(currentCell, cellPosition, i, j);
            }
        }
        
        
    }
}