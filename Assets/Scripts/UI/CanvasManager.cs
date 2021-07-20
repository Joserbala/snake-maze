using SnakeMaze.Audio;
using SnakeMaze.SO;
using SnakeMaze.SO.FoodSO;
using SnakeMaze.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject inGameHUDGroup;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private PlayerVariableSO player;
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI finalScoreLoose;
        [SerializeField] private TextMeshProUGUI finalScoreWin;
        [SerializeField] private TextMeshProUGUI finalGoldLoose;
        [SerializeField] private TextMeshProUGUI finalGoldWin;
        [SerializeField] private BusGameManagerSO gameManager;
        [SerializeField] private BusFoodSO busFoodSo;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button deathMenuButton;
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
            if (!gameManager.GameStarted || _isDeathPanelActive || _isPausePanelActive) return;
            tapRequest.PlayAudio();
            gameManager.PauseGame?.Invoke(true);
        }

        private void SwitchPausePanel(bool pause)
        {
            _isPausePanelActive = pause;
            pausePanel.SetActive(pause);
        }

        private void PressMenuButton()
        {
            gameManager.EndGame?.Invoke();
        }

        private void ResetPoints()
        {
            player.Points = 0;
            points.text = player.Points.ToString();
        }

        private void AddPoints(int amount)
        {
            player.Points += amount;
            points.text = player.Points.ToString();
        }

        private void OnPlayerWin()
        {
            SwitchWinPanel();
            OnWinSetFinalScore();
        }

        private void OnPlayerLoose()
        {
            SwitchDeathPanel();
            OnLooseSetFinalScore();
        }

        private void SwitchDeathPanel()
        {
            if (_isPausePanelActive) return;
            
            deathPanel.SetActive(!_isDeathPanelActive);
            _isDeathPanelActive = !_isDeathPanelActive;
        }

        private void SwitchWinPanel()
        {
            winPanel.SetActive(!_isWinPanelActive);
            _isWinPanelActive = !_isWinPanelActive;
        }

        /// <summary>
        /// Hides the ingame HUD and Sets the final score when the player wins.
        /// </summary>
        private void OnWinSetFinalScore()
        {
            inGameHUDGroup.SetActive(false);
            finalScoreWin.text = player.Points.ToString();
            finalGoldWin.text = EconomyManager.SetCoinsFromPoint(true, player.Points).ToString();
        }

        /// <summary>
        /// Hides the ingame HUD and sets the final score when the player loses.
        /// </summary>
        private void OnLooseSetFinalScore()
        {
            inGameHUDGroup.SetActive(false);
            finalScoreLoose.text = player.Points.ToString();
            finalGoldLoose.text = EconomyManager.SetCoinsFromPoint(false, player.Points).ToString();
        }

        private void OnEnable()
        {
            gameManager.PlayerDeath += OnPlayerLoose;
            gameManager.WinGame += OnPlayerWin;
            gameManager.PauseGame += SwitchPausePanel;
            resumeButton.onClick.AddListener(PressResumeButton);
            pauseButton.onClick.AddListener(PressPauseButton);
            menuButton.onClick.AddListener(PressMenuButton);
            deathMenuButton.onClick.AddListener(PressMenuButton);
            busFoodSo.OnEatFoodPoints += AddPoints;
        }

        private void OnDisable()
        {
            gameManager.PlayerDeath -= OnPlayerLoose;
            gameManager.WinGame -= OnPlayerWin;
            gameManager.PauseGame -= SwitchPausePanel;
            resumeButton.onClick.RemoveListener(PressResumeButton);
            pauseButton.onClick.RemoveListener(PressPauseButton);
            menuButton.onClick.RemoveListener(PressMenuButton);
            deathMenuButton.onClick.RemoveListener(PressMenuButton);
            busFoodSo.OnEatFoodPoints -= AddPoints;
        }
    }
}