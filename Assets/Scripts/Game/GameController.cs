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
        public string DifficultyName { get; set; }

        public GameController()
        {
        }

        public void Initialize()
        {
            Debug.Log("Initialize");
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
        }

        
    }
}