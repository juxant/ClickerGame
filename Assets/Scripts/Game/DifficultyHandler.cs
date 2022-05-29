using ClickerGame.Game.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClickerGame.Game
{
    public class DifficultyHandler : MonoBehaviour
    {
        [Inject] readonly SignalBus _signalBus;

        private Button[] _difficultyButtons;

        void Start()
        {
            _difficultyButtons = GetComponentsInChildren<Button>();
            foreach (var button in _difficultyButtons)
            {
                button.onClick.AddListener(() => _signalBus.Fire(new StartGameSignal(button.gameObject.name)));
            }
        }

        private void OnDestroy()
        {
            foreach (var button in _difficultyButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}