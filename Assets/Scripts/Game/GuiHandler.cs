using ClickerGame.Game.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClickerGame.Game
{
    public class GuiHandler : MonoBehaviour
    {
        [Inject] readonly GameController _gameController;
        [Inject] readonly SignalBus _signalBus;

        [SerializeField] GameObject _gameOverScreen;
        [SerializeField] Text _gameOverText;
        [SerializeField] Button _restartButton;

        [SerializeField] Text _timeText;
        [SerializeField] Text _pointsText;

        void Start()
        {
            _restartButton.onClick.AddListener(_gameController.RestartGame);
            _signalBus.Subscribe<GameOverSignal>(SetGameOverScreen);
        }

        void Update()
        {
            _timeText.text = _gameController.ElapsedTime.ToString("0.##");
            _pointsText.text = _gameController.TotalPoints.ToString("0.##");
        }

        void SetGameOverScreen(GameOverSignal gameOverSignal)
        {
            _gameOverText.text = gameOverSignal.GameoverText;
            _gameOverScreen.SetActive(true);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_gameController.RestartGame);
            _signalBus.Unsubscribe<GameOverSignal>(SetGameOverScreen);
        }
    }
}