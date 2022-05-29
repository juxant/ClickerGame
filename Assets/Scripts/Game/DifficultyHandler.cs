using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ClickerGame
{
    public class DifficultyHandler : MonoBehaviour
    {
        [Inject] readonly SignalBus _signalBus;

        void Start()
        {
            foreach (var button in GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(() => _signalBus.Fire(new StartGameSignal(button.gameObject.name)));
            }
        }
    }
}