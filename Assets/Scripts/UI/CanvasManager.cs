using SnakeMaze.Audio;
using SnakeMaze.SO;
using SnakeMaze.SO.FoodSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private PlayerVariableSO player;
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private BusGameManagerSO gameManager;
        [SerializeField] private BusFoodSO busFoodSo;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private AudioRequest tapRequest;
        private bool _isDeathPanelActive;
        private bool _isPausePanelActive;
        private bool _isWinPanelActive;

        private void Start()
        {
            _isDeathPanelActive = false;
            _isPausePanelActive = false;
            _isWinPanelActive = false;
            ResetPoints();
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
        private void SwitchWinPanel()
        {
            winPanel.SetActive(!_isWinPanelActive);
            _isWinPanelActive = !_isWinPanelActive;
        }
        private void SwitchPausePanel(bool pause)
        {
            pausePanel.SetActive(pause);
        }

        private void ResetPoints()
        {
            player.Poitns = 0;
            points.text = player.Poitns.ToString();
        }

        private void AddPoints(int amount)
        {
            player.Poitns += amount;
            points.text = player.Poitns.ToString();
        }

        private void OnEnable()
        {
            gameManager.EndGame += SwitchDeathPanel;
            gameManager.WinGame += SwitchWinPanel;
            gameManager.PauseGame += SwitchPausePanel;
            resumeButton.onClick.AddListener(PressResumeButton);
            pauseButton.onClick.AddListener(PressPauseButton);
            busFoodSo.OnEatFoodPoints += AddPoints;

        }

        private void OnDisable()
        {
            gameManager.EndGame -= SwitchDeathPanel;
            gameManager.WinGame -= SwitchWinPanel;
            gameManager.PauseGame -= SwitchPausePanel;
            resumeButton.onClick.RemoveListener(PressResumeButton);
            pauseButton.onClick.RemoveListener(PressPauseButton);
            busFoodSo.OnEatFoodPoints -= AddPoints;
        }
    }
}
