using System;
using SnakeMaze.Enums;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "PlayerVariable", menuName = "Scriptables/PlayerVariable")]
    public class PlayerVariableSO : ScriptableObject
    {
        [SerializeField] private float normalSpeed = 1;
        [SerializeField] private float boostSpeed = 2;
        [SerializeField] private float coroutineSeconds = 0.02f;
        [SerializeField] private int pixelsPerTile = 32;
        [SerializeField] private int playerPixels = 4;

        public Action<bool> startMoving;
        public Action<bool> isAlive;
        
        private float _horizontal;
        private float _vertical;
        private float _currentSpeed;
        private bool _isAlive;
        private bool _isMoving;
        private bool _start;
        private Directions _lastDirection;
        

        public float NormalSpeed
        {
            get=>normalSpeed;
            set=>normalSpeed=value;
        }
        public float BoostSpeed
        {
            get=>boostSpeed;
            set=>boostSpeed=value;
        }
        public float CoroutineSeconds
        {
            get=>coroutineSeconds;
            set=>coroutineSeconds=value;
        }

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }
        
        public int PixelsPerTile=>pixelsPerTile;
        public int PlayerPixels=>playerPixels;

        public float Horizontal
        {
            get => _horizontal;
            set => _horizontal=value;
        }
        public float Vertical
        {
            get => _vertical;
            set => _vertical=value;
        }

        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                _isMoving = value;
                startMoving?.Invoke(value);
                
            }
        }
        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                _isAlive = value;
                isAlive?.Invoke(value);
                
            }
        }

        public Directions LastDirection
        {
            get => _lastDirection;
            set => _lastDirection = value;
        }
    }
}
