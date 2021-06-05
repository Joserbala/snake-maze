using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace SnakeMaze
{
    public class Inputs : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private int normalSpeed = 1;
        [SerializeField] private int boostSpeed = 2;
        [SerializeField] private float coroutineSeconds = 0.02f;

        private const int PixelsPerTile = 32;
        private float _horizontal;
        private float _vertical;
        private float _currentSpeed;
        private Vector2 _direction;
        private Vector2 _currentDirection;

        private void Awake()
        {
#if UNITY_EDITOR
#else
            Debug.Log("Awake");
            InputSystem.EnableDevice(Gyroscope.current);
#endif
            StartCoroutine(Move());
        }

        private void Start()
        {
            _currentSpeed = normalSpeed;
            _currentDirection = Vector2.right;
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
            Debug.Log("Axis Vertical " + _horizontal);
        }

        public void GetVerticalValue(InputAction.CallbackContext ctx)
        {
            _vertical = ctx.ReadValue<float>();
            Debug.Log("Axis Horizontal " + _vertical);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _direction = value.ReadValue<Vector2>();
            Debug.Log("Getting Movement");
        }

        public void Boost(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                _currentSpeed = boostSpeed;
            if (ctx.canceled)
                _currentSpeed = normalSpeed;
            Debug.Log("Boost Velocity");
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

                transform.Translate(_currentSpeed / PixelsPerTile * _currentDirection.normalized, Space.World);
                yield return new WaitForSeconds(coroutineSeconds);
            }
        }
    }
}