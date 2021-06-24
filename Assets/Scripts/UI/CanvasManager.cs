using SnakeMaze.Audio;
using SnakeMaze.SO;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private BusGameManagerSO gameManager;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private AudioRequest tapRequest;
        private bool _isDeathPanelActive;
        private bool _isPausePanelActive;

        private void Start()
        {
            _isDeathPanelActive = false;
            _isPausePanelActive = false;
        }
        private void PressResumeButton()
        {
            if (!gameManager.GameStarted) return;
            tapRequest.PlayAudio();
            gameManager.PauseGame?.Invoke(false);
        }
        private void PressPauseButton()
        {
            if (!gameManager.GameStarted||_isDeathPanelActive||_isPausePanelActive) return;
            tapRequest.PlayAudio();
            gameManager.PauseGame?.Invoke(true);
        }

        private void SwitchDeathPanel()
        {
            deathPanel.SetActive(!_isDeathPanelActive);
            _isDeathPanelActive = !_isDeathPanelActive;
        }
        private void SwitchPausePanel(bool pause)
        {
            pausePanel.SetActive(pause);
        }

        private void OnEnable()
        {
            Debug.Log("Subscribing in canvas");
            gameManager.EndGame += SwitchDeathPanel;
            gameManager.PauseGame += SwitchPausePanel;
            resumeButton.onClick.AddListener(PressResumeButton);
            pauseButton.onClick.AddListener(PressPauseButton);
        }

        private void OnDisable()
        {
            gameManager.EndGame -= SwitchDeathPanel;
            gameManager.PauseGame -= SwitchPausePanel;
            resumeButton.onClick.RemoveListener(PressResumeButton);
            pauseButton.onClick.RemoveListener(PressPauseButton);
        }
    }
}
