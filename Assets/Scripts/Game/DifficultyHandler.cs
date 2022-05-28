using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace ClickerGame
{
    public class DifficultyHandler : MonoBehaviour
    {
        [Inject] GameController _gameController;

        void Start()
        {
            foreach (var button in GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(() => SetDifficulty(button.gameObject.name));
            }
        }

        void SetDifficulty(string difficultyName)
        {
            _gameController.DifficultyName = difficultyName;
            SceneManager.LoadScene(1);
        }
    }
}