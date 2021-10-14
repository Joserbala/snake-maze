using System;
using UnityEngine;

namespace SnakeMaze.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private bool showInEditor;

        private void Awake()
        {
            CheckDevice();
        }

        private void CheckDevice()
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            gameObject.SetActive(false);
#endif
#if UNITY_EDITOR
            gameObject.SetActive(showInEditor);
#endif
        }
    }
}