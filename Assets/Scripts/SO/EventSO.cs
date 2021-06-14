using System;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName ="EventSO", menuName = "Scriptables/EventSO")]
    public class EventSO : ScriptableObject
    {
        private Action _currentAction;

        public Action CurrentAction
        {
            get => _currentAction;
            set => _currentAction = value;
        }
    }
}
