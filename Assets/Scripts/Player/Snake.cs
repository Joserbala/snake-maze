using System;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Snake : PlayerPhysics
    {
        [SerializeField] private SnakeSkinSO currentSkin;
        private Directions _currentDirection;
        private Directions _lastDirection;
        private Sprite _currentSprite;
        private Sprite _lastSprite;
        private bool _isTail;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _isTail = true;
            _currentDirection = Directions.Right;
            _lastDirection = Directions.Right;
            if (_currentSprite != null)
                _spriteRenderer.sprite = _currentSprite;
        }

        public Sprite CurrentSprite
        {
            get => _currentSprite;
            set => _currentSprite = value;
        }
        public Sprite LastSprite
        {
            get => _lastSprite;
            set => _lastSprite = value;
        }

        public Directions CurrentDirection
        {
            get => _currentDirection;
            set => _currentDirection = value;
        }
        public Directions LastDirection
        {
            get => _lastDirection;
            set => _lastDirection = value;
        }
        
        public bool IsTail
        {
            get => _isTail;
            set => _isTail = value;
        }

        public void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}
