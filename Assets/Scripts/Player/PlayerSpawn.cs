using SnakeMaze.BSP;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeMaze.Player
{
    public class PlayerSpawn : MonoBehaviour
    {
        private BSPGenerator _bspGenerator;
        private Transform _player;
        private const int PixelsPerTile = 32;
        

        private void Awake()
        {
            _bspGenerator = FindObjectOfType<BSPGenerator>();
            _player = FindObjectOfType<PlayerController>().gameObject.transform;
        }

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var room = _bspGenerator.RoomList[Random.Range(0, _bspGenerator.RoomList.Count)];
            var pos = room.Grid.GetCellAtPosition(room.BottomLeftCorner,room.Center).Position;
            _player.position = pos;
        }
    }
}
