using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Player;
using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace SnakeMaze
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerVariableSO playerVariable;
        [SerializeField] private EventSO startGameEvent;
        private void Awake()
        {
#if UNITY_EDITOR
#else
            Debug.Log("Awake");
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
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
            playerVariable.Horizontal = ctx.ReadValue<float>();
            // playerVariable.Horizontal=(Accelerometer.current.acceleration.ReadValue()).x;
        }
        
        public void GetVerticalValue(InputAction.CallbackContext ctx)
        {
            playerVariable.Vertical = ctx.ReadValue<float>();
            // playerVariable.Vertical=(Accelerometer.current.acceleration.ReadValue()).y;
        }
        public void GetAcceleration(InputAction.CallbackContext ctx)
        {
            // playerVariable.Horizontal = ctx.ReadValue<Vector3>().x;
            // playerVariable.Vertical = ctx.ReadValue<Vector3>().y;
        }
        
        

        public void Boost(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
               playerVariable.CurrentSpeed = playerVariable.BoostSpeed;
            }

            if (ctx.canceled)
            {
                playerVariable.CurrentSpeed = playerVariable.NormalSpeed;
                if (!playerVariable.IsMoving)
                    playerVariable.IsMoving = true;
            }
        }
    }
}