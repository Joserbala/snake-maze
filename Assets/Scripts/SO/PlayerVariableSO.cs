using System;
using SnakeMaze.Enums;
using SnakeMaze.Interfaces;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "PlayerVariable", menuName = "Scriptables/PlayerVariable")]
    public class PlayerVariableSO : ResseteableSO
    {
        [SerializeField] private float normalSpeed = 1;
        [SerializeField] private float boostSpeed = 2;
        [SerializeField] private float coroutineSeconds = 0.02f;
        [SerializeField] private float minimunCoroutineSec = 0.001f;
        [SerializeField] private float speedChangeAmount = 0.01f;
        [SerializeField] private float changeSpeedRate = 5f;
        [SerializeField] private int pixelsPerTile = 32;
        [SerializeField] private int playerPixels = 4;

        private float _currentCoroutineSeconds;
        private float _horizontal;
        private float _vertical;
        private float _currentSpeed;
        private bool _isAlive;
        private bool _isMoving;
        private Directions _lastDirection;
        private Directions _currentDirection;
        private float _poitns;


        public float NormalSpeed
        {
            get => normalSpeed;
        }
        public float BoostSpeed
        {
            get => boostSpeed;
        }
        public float CurrentCoroutineSeconds
        {
            get => _currentCoroutineSeconds;
            set => _currentCoroutineSeconds = value;
        }
        public float SpeedChangeAmount
        {
            get => speedChangeAmount;
            set => speedChangeAmount = value;
        }
        public float ChangeSpeedRate
        {
            get => changeSpeedRate;
            set => changeSpeedRate = value;
        }
        public float MinimunCoroutineSec
        {
            get => minimunCoroutineSec;
            set => minimunCoroutineSec = value;
        }

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        public int PixelsPerTile => pixelsPerTile;
        public int PlayerPixels => playerPixels;

        public float Horizontal
        {
            get => _horizontal;
            set => _horizontal = value;
        }
        public float Vertical
        {
            get => _vertical;
            set => _vertical = value;
        }

        public bool IsMoving
        {
            get => _isMoving;
            set => _isMoving = value;

        }
        public bool IsAlive
        {
            get => _isAlive;
            set => _isAlive = value;

        }

        public Directions LastDirection
        {
            get => _lastDirection;
            set => _lastDirection = value;
        }

        public Directions CurrentDirection
        {
            get => _currentDirection;
            set => _currentDirection = value;
        }

        public float Points
        {
            get => _poitns;
            set => _poitns = value;
        }

        public override void ResetValues()
        {
            _currentCoroutineSeconds = coroutineSeconds;
            _poitns = 0;
        }
    }
}
