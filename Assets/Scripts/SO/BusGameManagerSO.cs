using System;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "GameManagerSO", menuName = "Scriptables/BusSO/BusGameManagerSO")]
    public class BusGameManagerSO : ScriptableObject
    {
        public Action StartGame;
        public Action EndGame;
        public Action<bool> PauseGame;
        private bool _gameStarted;

        public bool GameStarted
        {
            get => _gameStarted;
            set => _gameStarted = value;
        }

        private void OnEnable()
        {
            StartGame += SetGameStartedTrue;
            EndGame += SetGameStartedFalase;
        }

        private void OnDisable()
        {
            StartGame -= SetGameStartedTrue;
            EndGame -= SetGameStartedFalase;
        }

        public void ResetValues()
        {
            _gameStarted = false;
        }

        private void SetGameStartedTrue() => _gameStarted = true;
        private void SetGameStartedFalase() => _gameStarted = false;
    }
}
