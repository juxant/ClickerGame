﻿using System;
using Zenject;

namespace ClickerGame
{
    public class Coin : Item
    {
        Settings _settings;

        [Inject]
        public void Construct(Settings settings)
        {
            base.Construct(settings);
            _settings = settings;
        }

        [Serializable]
        public new class Settings : Item.Settings
        {
        }

        public class Factory : PlaceholderFactory<Coin>
        {
        }
    }
}