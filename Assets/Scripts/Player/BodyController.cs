using System;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze.Player
{
    public class BodyController : MonoBehaviour
    {
        [SerializeField] private GameObject snakePrefab;
        [SerializeField] private SnakeSkinSO currentSkin;
        [SerializeField] private Transform headPosition;
        [SerializeField] private PlayerVariableSO player;
        private List<Snake> snakeParts = new List<Snake>();

        private void Start()
        {
            Invoke(nameof(InitBody), 0.5f);
        }

        private void InitBody()
        {
            InstantiateBody(headPosition.position - Vector3.right * player.PlayerPixels / player.PixelsPerTile,
                Directions.Right, Directions.Right);
            snakeParts[0].IsTail = false;
            snakeParts[0].CurrentSprite = currentSkin.SnakeSkin.Body.Right;
            snakeParts[0].LastSprite = currentSkin.SnakeSkin.Body.Right;
            snakeParts[0].UpdateSprite(snakeParts[0].CurrentSprite);
            InstantiateBody(headPosition.position - Vector3.right * 2 * player.PlayerPixels / player.PixelsPerTile,
                Directions.Right, Directions.Right);
            snakeParts[1].IsTail = false;
            snakeParts[1].CurrentSprite = currentSkin.SnakeSkin.Body.Right;
            snakeParts[1].LastSprite = currentSkin.SnakeSkin.Body.Right;
            snakeParts[1].UpdateSprite(snakeParts[1].CurrentSprite);
            InstantiateBody(headPosition.position - Vector3.right * 3 * player.PlayerPixels / player.PixelsPerTile,
                Directions.Right, Directions.Right);
            snakeParts[2].CurrentSprite = currentSkin.SnakeSkin.Tail.Right;
            snakeParts[2].LastSprite = currentSkin.SnakeSkin.Tail.Right;
            snakeParts[2].UpdateSprite(snakeParts[2].CurrentSprite);
        }

        private void InstantiateBody(Vector2 position, Directions currentDirections, Directions lastDirections)
        {
            var body = Instantiate(snakePrefab, position, Quaternion.identity, transform);
            var snake = body.GetComponent<Snake>();
            snake.LastDirection = lastDirections;
            snake.CurrentDirection = currentDirections;
            snake.CurrentSprite = currentSkin.SnakeSkin.Tail.Right;
            snake.LastSprite = currentSkin.SnakeSkin.Tail.Right;

            snakeParts.Add(snake);
        }

        public void MoveSnakeBody()
        {
            snakeParts[snakeParts.Count - 1].Move(snakeParts[snakeParts.Count - 2].LastDirection);


            for (int i = snakeParts.Count - 2; i >= 1; i--)
            {
                snakeParts[i].Move(snakeParts[i - 1].LastDirection);
            }

            snakeParts[0].Move(player.LastDirection);

            snakeParts[0].LastDirection = snakeParts[0].CurrentDirection;
            snakeParts[0].CurrentDirection = player.CurrentDirection;
            for (int i = 1; i < snakeParts.Count - 1; i++)
            {
                snakeParts[i].LastDirection = snakeParts[i].CurrentDirection;
                snakeParts[i].CurrentDirection = snakeParts[i - 1].LastDirection;
            }

            snakeParts[snakeParts.Count - 1].LastDirection = snakeParts[snakeParts.Count - 1].CurrentDirection;
            snakeParts[snakeParts.Count - 1].CurrentDirection = snakeParts[snakeParts.Count - 2].LastDirection;

            snakeParts[0].LastSprite = snakeParts[0].CurrentSprite;
            snakeParts[0].CurrentSprite = GetActualBodySprite(snakeParts[1].CurrentDirection,
                player.CurrentDirection);
            snakeParts[0].UpdateSprite(snakeParts[0].CurrentSprite);

            for (int i = snakeParts.Count - 2; i >= 1; i--)
            {
                snakeParts[i].LastSprite = snakeParts[i].CurrentSprite;
                snakeParts[i].CurrentSprite = snakeParts[i - 1].LastSprite;
                snakeParts[i].UpdateSprite(snakeParts[i].CurrentSprite);
            }
            snakeParts[snakeParts.Count - 1].LastSprite = snakeParts[snakeParts.Count - 1].CurrentSprite;
            snakeParts[snakeParts.Count - 1].CurrentSprite =
                GetActualTailSprite(snakeParts[snakeParts.Count - 2].LastDirection);
            snakeParts[snakeParts.Count - 1].UpdateSprite(snakeParts[snakeParts.Count - 1].CurrentSprite);

            
        }

        private Sprite GetActualTailSprite(Directions followingDirection)
        {
            var sprite = followingDirection switch
            {
                Directions.Up => currentSkin.SnakeSkin.Tail.Up,
                Directions.Down => currentSkin.SnakeSkin.Tail.Down,
                Directions.Right => currentSkin.SnakeSkin.Tail.Right,
                Directions.Left => currentSkin.SnakeSkin.Tail.Left,
                _ => currentSkin.SnakeSkin.Tail.Up
            };
            return sprite;
        }

        private Sprite GetActualBodySprite(Directions previousDir, Directions followingDir)
        {
            Sprite sprite = null;
            if ((Mathf.Abs((int) previousDir) == 1 && Mathf.Abs((int) followingDir) == 2) ||
                (Mathf.Abs((int) previousDir) == 2 && Mathf.Abs((int) followingDir) == 1))
            {
                if (previousDir == Directions.Down)
                {
                    sprite = followingDir == Directions.Left
                        ? currentSkin.SnakeSkin.Body.CornerTopLeft
                        : currentSkin.SnakeSkin.Body.CornerTopRight;
                }

                if (previousDir == Directions.Up)
                {
                    sprite = followingDir == Directions.Left
                        ? currentSkin.SnakeSkin.Body.CornerBottomLeft
                        : currentSkin.SnakeSkin.Body.CornerBottomRight;
                }

                if (previousDir == Directions.Right)
                {
                    sprite = followingDir == Directions.Down
                        ? currentSkin.SnakeSkin.Body.CornerBottomLeft
                        : currentSkin.SnakeSkin.Body.CornerTopLeft;
                }

                if (previousDir == Directions.Left)
                {
                    sprite = followingDir == Directions.Down
                        ? currentSkin.SnakeSkin.Body.CornerBottomRight
                        : currentSkin.SnakeSkin.Body.CornerTopRight;
                }
            }
            //If previous dir is horizontal
            else if (Mathf.Abs((int) previousDir) == 2)
            {
                sprite = followingDir == Directions.Left
                    ? currentSkin.SnakeSkin.Body.Left
                    : currentSkin.SnakeSkin.Body.Right;
            }
            else
            {
                sprite = followingDir == Directions.Down
                    ? currentSkin.SnakeSkin.Body.Down
                    : currentSkin.SnakeSkin.Body.Up;
            }

            return sprite;
        }
    }
}