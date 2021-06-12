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
            snakeParts[0].CurrentSprite = currentSkin.SnakeSkin.Body.Horizontal;
            snakeParts[0].UpdateSprite(snakeParts[0].CurrentSprite);
            InstantiateBody(headPosition.position - Vector3.right * 2 * player.PlayerPixels / player.PixelsPerTile,
                Directions.Right, Directions.Right);
            snakeParts[1].CurrentSprite = currentSkin.SnakeSkin.Tail.Horizontal;
            snakeParts[1].UpdateSprite(snakeParts[1].CurrentSprite);
        }

        private void InstantiateBody(Vector2 position, Directions currentDirections, Directions lastDirections)
        {
            var body = Instantiate(snakePrefab, position, Quaternion.identity, transform);
            var snake = body.GetComponent<Snake>();
            snake.LastDirection = lastDirections;
            snake.CurrentDirection = currentDirections;

            snakeParts.Add(snake);
        }

        public void MoveSnakeBody()
        {
            snakeParts[0].Move(player.LastDirection);
            for (int i = 1; i < snakeParts.Count; i++)
            {
                snakeParts[i].Move(snakeParts[i - 1].LastDirection);
            }

            var flipX = snakeParts[0].FlipX;
            var flipY = snakeParts[0].FlipY;

            snakeParts[0].CurrentSprite = GetActualSprite(snakeParts[0].CurrentDirection,
                snakeParts[1].CurrentDirection, player.LastDirection,
                ref flipX, ref flipY);
            // snakeParts[0].

        }

        private Sprite GetActualSprite(Directions myDir, Directions previousDir, Directions followingDir,
            ref bool flipX, ref bool flipY)
        {
            Sprite sprite = null;
            //If previous direction is horizontal and following is vertical or vice versa
            if (Mathf.Abs((int)previousDir) == 1 && Mathf.Abs((int)followingDir) == 2 ||
                (Mathf.Abs((int)previousDir) == 2 &&  Mathf.Abs((int)followingDir) == 1))
            {
                sprite = currentSkin.SnakeSkin.Body.Corner;
                //If previous dir is horizontal
                if (followingDir == Directions.Left)
                    flipX = true;
                

                if (followingDir == Directions.Up)
                    flipY = true;

            }
            //If previous dir is horizontal
            else if (Mathf.Abs((int)previousDir) == 2)
            {
                sprite = currentSkin.SnakeSkin.Body.Horizontal;
                if (followingDir == Directions.Left)
                {
                    flipX = true;
                    flipY = false;
                }
            }
            else
            {
                sprite = currentSkin.SnakeSkin.Body.Vertical;
                if (followingDir == Directions.Down)
                {
                    flipX = false;
                    flipY = true;
                }
            }

            return sprite;
        }
    }
}