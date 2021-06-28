using SnakeMaze.Audio;
using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeMaze.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerVariableSO playerVariable;
        [SerializeField] private BusGameManagerSO gameManager;
        [SerializeField] private AudioRequest boostIn;
        [SerializeField] private AudioRequest boostOut;
        private bool _isOnBoost;
        private void Awake()
        {
            _isOnBoost = false;
        }
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
                if(gameManager.GameStarted)
                {
                    playerVariable.CurrentSpeed = playerVariable.BoostSpeed;
                    boostIn.PlayAudio();
                    _isOnBoost = true;
                }
            }

            if (ctx.started)
            {
                if (!gameManager.GameStarted && playerVariable.IsAlive)
                    gameManager.StartGame?.Invoke();
            }
        }

        public void ResetVel(InputAction.CallbackContext ctx)
        {
            if (!gameManager.GameStarted) return;
            if (ctx.performed)
            {
                if (!_isOnBoost) return;
                _isOnBoost = false;
                playerVariable.CurrentSpeed = playerVariable.NormalSpeed;
                boostOut.PlayAudio();
            }
        }
    }
}