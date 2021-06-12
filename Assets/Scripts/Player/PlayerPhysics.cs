using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.Maze;
using UnityEngine;

namespace SnakeMaze.Player
{
    public class PlayerPhysics : MonoBehaviour
    {
        
        [SerializeField] private LayerMask collisionLayer;
        private const int PixelsPerTile = 32;
        private const int PlayerPixel = 4;

        public virtual void Move(Vector2 dir)
        {
            transform.Translate(PlayerPixel*1f / PixelsPerTile * dir.normalized, Space.World);
        }
        public virtual void Move(Directions direction)
        {
            Vector2 dir =DirectionsActions.DirectionsToVector2(direction);
            transform.Translate(PlayerPixel*1f / PixelsPerTile * dir, Space.World);
        }

        protected bool CheckCollision(Vector2 direction)
        {
            var collided = false;
            var hit = Physics2D.Raycast(transform.position, direction,
                (PlayerPixel/2f/PixelsPerTile*1.5f), collisionLayer);
            if (hit)
            {
                collided = hit.collider.CompareTag("Wall");
                Debug.Log("Hitted");
            }
            return collided;
        }
    }
}
