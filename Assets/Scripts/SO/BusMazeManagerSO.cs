using System;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "MazeManagerSO", menuName = "Scriptables/BusSO/BusMazeManagerSO")]
    public class BusMazeManagerSO : ScriptableObject
    {
        public Action StartMaze;
        public Action FinishMaze;
        private bool _isMazeFinished;

        public bool IsMazeFinished
        {
            get => _isMazeFinished;
            set => _isMazeFinished = value;
        }
        private void OnEnable()
        {
            StartMaze += SetIsMazeFinishedTrue;
            FinishMaze += SetIsMazeFinishedFalse;
        }

        private void OnDisable()
        {
            StartMaze -= SetIsMazeFinishedTrue;
            FinishMaze -= SetIsMazeFinishedFalse;
        }

        public void ResetValues()
        {
            _isMazeFinished = false;
        }

        private void SetIsMazeFinishedTrue() => _isMazeFinished = true;
        private void SetIsMazeFinishedFalse() => _isMazeFinished = false;
    }
}
