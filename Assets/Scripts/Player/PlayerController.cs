using System.Collections;
using SnakeMaze.Audio;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : PlayerPhysics
    {
        [SerializeField] private PlayerVariableSO playerVariable;
        [SerializeField] private SkinContainerSO skinContainer;
        [SerializeField] private BusGameManagerSO gameManager;
        [SerializeField] private AudioRequest deathRequest;

        private BodyController _bodyController;
        private SpriteRenderer _spriteRenderer;
        private IEnumerator _moveCoroutine;
        private IEnumerator _changeSpeedCoroutine;
        private Directions _currentDirection=Directions.Right;

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
            playerVariable.CurrentDirection = Directions.Right;
            playerVariable.IsAlive = true;
            playerVariable.IsMoving = false;
        }

        private void StartMoving()
        {
            playerVariable.IsMoving = true;
            _moveCoroutine = Move();
            StartCoroutine(_moveCoroutine);
        }

        private void StopMoving()
        {
            playerVariable.IsMoving = false;
            StopCoroutine(_moveCoroutine);
        }

        private void StartChangingSpeed()
        {
            _changeSpeedCoroutine = ChangeSpeed();
            StartCoroutine(_changeSpeedCoroutine);
        }

        private void StopChangingSpeed()
        {
            StopCoroutine(_changeSpeedCoroutine);
        }

        private void SetMoving(bool pause)
        {
            if(pause)
                StopMoving();
            else
                StartMoving();
        }
        private void SetChangingSpeed(bool pause)
        {
            if(pause)
                StopChangingSpeed();
            else
                StartChangingSpeed();
        }

        private IEnumerator ChangeSpeed()
        {
            while (playerVariable.CurrentCoroutineSeconds>playerVariable.MinimunCoroutineSec)
            {
                yield return new WaitForSeconds(playerVariable.ChangeSpeedRate);
                playerVariable.CurrentCoroutineSeconds -= playerVariable.SpeedChangeAmount;
            }
        }

        private IEnumerator Move()
        {
            while (true)
            {
                playerVariable.LastDirection = _currentDirection;
                if (playerVariable.Horizontal != 0 || playerVariable.Vertical != 0)
                {
                    var direction = Mathf.Abs(playerVariable.Horizontal) >= Mathf.Abs(playerVariable.Vertical)
                        ? (Directions)((int)Directions.Right * Mathf.Sign(playerVariable.Horizontal))
                        : (Directions)((int)Directions.Up * Mathf.Sign(playerVariable.Vertical));
                   
                        
                    
                    if (direction != _currentDirection &&
                        direction!=DirectionsActions.GetOppositeDirection(_currentDirection))
                    {
                        
                        playerVariable.CurrentDirection = direction;
                        _currentDirection = direction;
                        
                    }
                    SetHeadSprite(_currentDirection);
                }
                if (CheckCollision(DirectionsActions.DirectionsToVector2(_currentDirection)))
                {
                    Die();
                    yield break;
                }
                Move(_currentDirection);
                _bodyController.MoveSnakeBody();
                var time = playerVariable.CurrentCoroutineSeconds / playerVariable.CurrentSpeed;
                yield return new WaitForSeconds(time);
            }
        }

        private void SetHeadSprite(Directions direction)
        {
            Sprite currentSprite = null;
            switch (direction)
            {
                case Directions.Up:
                    currentSprite = skinContainer.CurrentSnakeSkin.SnakeSkin.Head.Up;
                    break;
                case Directions.Down:
                    currentSprite = skinContainer.CurrentSnakeSkin.SnakeSkin.Head.Down;
                    break;
                case Directions.Right:
                    currentSprite = skinContainer.CurrentSnakeSkin.SnakeSkin.Head.Right;
                    break;
                case Directions.Left:
                    currentSprite = skinContainer.CurrentSnakeSkin.SnakeSkin.Head.Left;
                    break;
            }

            _spriteRenderer.sprite = currentSprite;
            
        }
// #if UNITY_EDITOR
//         private void OnDrawGizmos()
//         {
//             
//             Gizmos.color = Color.green;
//             Gizmos.DrawLine(transform.position,
//                 transform.position + (Vector3) DirectionsActions.DirectionsToVector2(_currentDirection)  *
//                 (playerVariable.PlayerPixels / 2f / playerVariable.PixelsPerTile * 1.5f));
//         }
// #endif

        private void Die()
        {
            playerVariable.IsAlive = false;
            gameManager.GameStarted = false;
            deathRequest.PlayAudio();
            gameManager.EndGame?.Invoke();
        }

        private void OnEnable()
        {
            gameManager.StartGame += StartMoving;
            gameManager.EndGame += StopMoving;
            gameManager.WinGame += StopMoving;
            gameManager.PauseGame += SetMoving;
            gameManager.StartGame += StartChangingSpeed;
            gameManager.EndGame += StopChangingSpeed;
            gameManager.WinGame += StopChangingSpeed;
            gameManager.PauseGame += SetChangingSpeed;
            gameManager.EndGame += StopAllCoroutines;
        }

        private void OnDisable()
        {
            gameManager.StartGame -= StartMoving;
            gameManager.EndGame -= StopMoving;
            gameManager.WinGame -= StopMoving;
            gameManager.PauseGame -= SetMoving;
            gameManager.StartGame -= StartChangingSpeed;
            gameManager.EndGame -= StopChangingSpeed;
            gameManager.WinGame -= StopChangingSpeed;
            gameManager.PauseGame -= SetChangingSpeed;
            gameManager.EndGame -= StopAllCoroutines;
        }
    }
}