using System;
using System.Collections;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : PlayerPhysics
    {
        [SerializeField] private PlayerVariableSO playerVariable;
        [SerializeField] private SnakeSkinSO currentSkin;

        private BodyController _bodyController;
        private SpriteRenderer _spriteRenderer;
        private Directions _currentDirection;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _bodyController = FindObjectOfType<BodyController>();
        }

        private void Start()
        {
            _currentDirection = Directions.Right;
            playerVariable.CurrentSpeed = playerVariable.NormalSpeed;
            playerVariable.LastDirection = Directions.Right;
            playerVariable.IsAlive = true;
            playerVariable.IsMoving = false;
        }

        private void StartMoving(bool move)
        {
            if (!move) return;
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (true)
            {
                if (playerVariable.Horizontal != 0 || playerVariable.Vertical != 0)
                {
                    var direction = Mathf.Abs(playerVariable.Horizontal) >= Mathf.Abs(playerVariable.Vertical)
                        ? (Directions)((int)Directions.Right * playerVariable.Horizontal)
                        : (Directions)((int)Directions.Up * playerVariable.Vertical);
                    playerVariable.LastDirection = _currentDirection;
                    if (direction != _currentDirection)
                    {
                        
                        _currentDirection = direction;
                        SetHeadSprite(direction);
                    }
                }

                if (CheckCollision(DirectionsActions.DirectionsToVector2(_currentDirection)))
                {
                    Die();
                    yield break;
                }

                Move(_currentDirection);
                _bodyController.MoveSnakeBody();
                var time = playerVariable.CoroutineSeconds / playerVariable.CurrentSpeed;
                yield return new WaitForSeconds(time);
            }
        }

        private void SetHeadSprite(Directions direction)
        {
            Sprite currentSprite = null;
            switch (direction)
            {
                case Directions.Up:
                    currentSprite = currentSkin.SnakeSkin.Head.Vertical;
                    _spriteRenderer.flipY = false;
                    _spriteRenderer.flipX = false;
                    break;
                case Directions.Down:
                    currentSprite = currentSkin.SnakeSkin.Head.Vertical;
                    _spriteRenderer.flipY = true;
                    _spriteRenderer.flipX = false;
                    break;
                case Directions.Right:
                    currentSprite = currentSkin.SnakeSkin.Head.Horizontal;
                    _spriteRenderer.flipY = false;
                    _spriteRenderer.flipX = false;
                    break;
                case Directions.Left:
                    currentSprite = currentSkin.SnakeSkin.Head.Horizontal;
                    _spriteRenderer.flipY = false;
                    _spriteRenderer.flipX = true;
                    break;
            }

            _spriteRenderer.sprite = currentSprite;
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,
                transform.position + (Vector3) DirectionsActions.DirectionsToVector2(_currentDirection)  *
                (playerVariable.PlayerPixels / 2f / playerVariable.PixelsPerTile * 1.5f));
        }

        private void Die()
        {
            playerVariable.IsAlive = false;
        }

        private void OnEnable()
        {
            playerVariable.startMoving += StartMoving;
        }

        private void OnDisable()
        {
            playerVariable.startMoving -= StartMoving;
        }
    }
}