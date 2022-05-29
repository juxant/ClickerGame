using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace ClickerGame
{
    public class GameController : ITickable, IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Settings _settings;

        bool _isPlaying = true;

        public float ElapsedTime { get; private set; }
        public float TotalPoints { get; private set; }
        public string DifficultyName { get; private set; }

        public GameController(SignalBus signalBus,
                            Settings settings)
        {
            _signalBus = signalBus;
            _settings = settings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(StartGame);
            _signalBus.Subscribe<ItemDestroyedSignal>(UpdatePoints);
        }

        public void Tick()
        {
            if (_isPlaying)
            {
                UpdateTime();
            }
        }

        void UpdateTime()
        {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= _settings.MaxTimeToPlay)
            {
                ElapsedTime = _settings.MaxTimeToPlay;
                _isPlaying = false;
                _signalBus.Fire(new GameOverSignal("You lose"));
            }
        }

        public void RestartGame()
        {
            _isPlaying = false;
            ElapsedTime = 0f;
            TotalPoints = 0f;
            SceneManager.LoadScene(0);
        }

        void StartGame(StartGameSignal startGameSignal)
        {
            _isPlaying = true;
            ElapsedTime = 0f;
            TotalPoints = 0f;
            DifficultyName = startGameSignal.DifficultyName;
            SceneManager.LoadScene(1);
        }

        void UpdatePoints(ItemDestroyedSignal itemDestroyedSignal)
        {
            TotalPoints += itemDestroyedSignal.Points;
            if (TotalPoints >= _settings.PointsToWin)
            {
                _isPlaying = false;
                _signalBus.Fire(new GameOverSignal("You won"));
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(StartGame);
            _signalBus.Unsubscribe<ItemDestroyedSignal>(UpdatePoints);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MaxTimeToPlay { get; private set; } = 120f; 
            [field: SerializeField] public float PointsToWin { get; private set; } = 200f; 
        }
    }
}