using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Player;
using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace SnakeMaze.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerVariableSO playerVariable;
        [SerializeField] private BusGameManagerSO gameManager;

        private void Awake()
        {
#if UNITY_EDITOR
#else
            Debug.Log("Awake");
            // InputSystem.EnableDevice(Gyroscope.current);
            // InputSystem.EnableDevice(Accelerometer.current);
            // InputSystem.EnableDevice(LinearAccelerationSensor.current);
#endif
        }


#if UNITY_EDITOR
#else
        // public void ConnectMobile()
        // {
        //     if (!Gyroscope.current.enabled)
        //         InputSystem.EnableDevice(Gyroscope.current);
        // }
        // public void DisconnectMobile()
        // {
        //     InputSystem.DisableDevice(Gyroscope.current);
        // }
#endif
        public void GetHorizontalValue(InputAction.CallbackContext ctx)
        {
// #if UNITY_ANDROID
//             value = (ctx.ReadValue<float>() / Screen.currentResolution.width - 0.5f) * 2f;
// #endif
#if UNITY_EDITOR
             float value = 0;
            value = ctx.ReadValue<float>();
             playerVariable.Horizontal = value;
#endif
            // playerVariable.Horizontal=(Accelerometer.current.acceleration.ReadValue()).x;
        }

        public void GetVerticalValue(InputAction.CallbackContext ctx)
        {
// #if UNITY_ANDROID
//             value = (ctx.ReadValue<float>() / Screen.currentResolution.height - 0.5f) * 2f;
// #endif
#if UNITY_EDITOR
            float value = 0;
            value = ctx.ReadValue<float>();
             playerVariable.Vertical = value;
#endif
            // playerVariable.Vertical=(Accelerometer.current.acceleration.ReadValue()).y;
        }

        public void GetAcceleration(InputAction.CallbackContext ctx)
        {
            // playerVariable.Horizontal = ctx.ReadValue<Vector3>().x;
            // playerVariable.Vertical = ctx.ReadValue<Vector3>().y;
        }

        public void GetDelta(InputAction.CallbackContext ctx)
        {
#if UNITY_ANDROID
            playerVariable.Horizontal = ctx.ReadValue<Vector2>().x;
            playerVariable.Vertical = ctx.ReadValue<Vector2>().y;
#endif
        }


        public void Boost(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                playerVariable.CurrentSpeed = playerVariable.BoostSpeed;
                Debug.Log("Boost");
            }

            if (ctx.started)
            {
                if (!gameManager.GameStarted && playerVariable.IsAlive)
                    gameManager.StartGame?.Invoke();
                Debug.Log("Start Game");
            }
        }

        public void ResetVel(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                playerVariable.CurrentSpeed = playerVariable.NormalSpeed;
            }
        }
    }
}