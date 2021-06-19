using System;
using SnakeMaze.BSP;
using SnakeMaze.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeMaze.Player
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField] private BusMazeManagerSO mazeManager;
        private BSPGenerator _bspGenerator;
        private Transform _player;


        private void Awake()
        {
            _bspGenerator = FindObjectOfType<BSPGenerator>();
            _player = FindObjectOfType<PlayerController>().gameObject.transform;
        }

        private void SpawnPlayer()
        {
            var room = _bspGenerator.RoomList[Random.Range(0, _bspGenerator.RoomList.Count)];
            var pos = room.Grid.GetCellAtPosition(room.BottomLeftCorner,room.Center).Position;
            _player.position = pos;
        }

        private void OnEnable()
        {
            mazeManager.FinishMaze += SpawnPlayer;
        }

        private void OnDisable()
        {
            mazeManager.FinishMaze -= SpawnPlayer;
        }
    }
}
