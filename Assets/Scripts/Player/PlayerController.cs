using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace SnakeMaze
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float normalSpeed = 1;
        [SerializeField] private float boostSpeed = 2;
        [SerializeField] private float coroutineSeconds = 0.02f;
        [SerializeField] private LayerMask wallLayer;

        private const int PixelsPerTile = 32;
        private const int PlayerPixel = 4;
        private float _horizontal;
        private float _vertical;
        private float _currentSpeed;
        private Vector2 _direction;
        private Vector2 _currentDirection;
        private bool _isMoving;
        private bool _isAlive;

        private void Awake()
        {
#if UNITY_EDITOR
#else
            Debug.Log("Awake");
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
            InputSystem.EnableDevice(GravitySensor.current);
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
#endif
        }

        private void Start()
        {
            _currentSpeed = normalSpeed;
            _currentDirection = Vector2.right;
            _isAlive = true; 
        }


#if UNITY_EDITOR
#else

        public void ConnectMobile()
        {
            if (!Gyroscope.current.enabled)
                InputSystem.EnableDevice(Gyroscope.current);
        }
        public void DisconnectMobile()
        {
            InputSystem.DisableDevice(Gyroscope.current);
        }
#endif
        public void GetHorizontalValue(InputAction.CallbackContext ctx)
        {
            _horizontal = ctx.ReadValue<float>();
                StartMoving();
            // Debug.Log("Axis Vertical " + _horizontal);
        }

        public void GetVerticalValue(InputAction.CallbackContext ctx)
        {
            _vertical = ctx.ReadValue<float>();
                StartMoving();
            // Debug.Log("Axis Horizontal " + _vertical);
        }
        // public void GetHorizontalValueAcc(InputAction.CallbackContext ctx)
        // {
        //     _horizontal = ctx.ReadValue<float>();
        //     StartMoving();
        //     // Debug.Log("Axis Vertical " + _horizontal);
        // }
        //
        // public void GetVerticalValueAcc(InputAction.CallbackContext ctx)
        // {
        //     _vertical = ctx.ReadValue<float>();
        //     StartMoving();
        //     // Debug.Log("Axis Horizontal " + _vertical);
        // }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _direction = value.ReadValue<Vector2>();
            StartMoving();
            // Debug.Log("Getting Movement");
        }

        public void Boost(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _currentSpeed = boostSpeed;
            }

            if (ctx.canceled)
            {
                _currentSpeed = normalSpeed;
                StartMoving();
            }
            // Debug.Log("Boost Velocity");
        }

        private void StartMoving()
        {
            if (_isMoving) return;
            _isMoving = true;
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (true)
            {
                if (_horizontal != 0 || _vertical != 0)
                {
                    var direction = Mathf.Abs(_horizontal) >= Mathf.Abs(_vertical)
                        ? Vector2.right * _horizontal
                        : Vector2.up * _vertical;
                    _currentDirection = direction;
                }
                if(CheckWall(_currentDirection))
                {
                    Die();
                    yield break;
                }

                var time = coroutineSeconds / _currentSpeed;
                transform.Translate(PlayerPixel*1f / PixelsPerTile * _currentDirection.normalized, Space.World);
                yield return new WaitForSeconds(time);
            }
        }

        private bool CheckWall(Vector2 direction)
        {
            bool wall = false;
            var hit = Physics2D.Raycast(transform.position, direction,
                (PlayerPixel/2f/PixelsPerTile*1.5f), wallLayer);
            if (hit)
            {
                wall = hit.collider.CompareTag("Wall");
                Debug.Log("Hitted");
            }
            return wall;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.green;
            Gizmos.DrawLine(transform.position,transform.position + (Vector3)_currentDirection*(PlayerPixel/2f/PixelsPerTile*1.5f));
        }

        private void Die()
        {
            _isAlive = false;
            Debug.Log("Die");
        }
    }
}