using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.SO;
using UnityEngine;

namespace SnakeMaze
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private EventSO playerDeath;
        private bool _isDeathPanelActive;

        private void Start()
        {
            _isDeathPanelActive = false;
        }

        private void OnEnable()
        {
            playerDeath.CurrentAction += SwitchDeathPanel;
        }

        private void OnDisable()
        {
            playerDeath.CurrentAction -= SwitchDeathPanel;
        }

        private void SwitchDeathPanel()
        {
            deathPanel.SetActive(!_isDeathPanelActive);
            _isDeathPanelActive = !_isDeathPanelActive;
            Debug.Log("Dead");
        }
    }
}
