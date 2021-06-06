using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.BSP;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeMaze
{
    public class PlayerSpawn : MonoBehaviour
    {
        private BSPGenerator _bspGenerator;
        private Transform _player;

        private void Awake()
        {
            _bspGenerator = FindObjectOfType<BSPGenerator>();
            _player = FindObjectOfType<Inputs>().gameObject.transform;
        }

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var pos = _bspGenerator.RoomList[Random.Range(0, _bspGenerator.RoomList.Count)].Center;
            _player.position = pos;
        }
    }
}
