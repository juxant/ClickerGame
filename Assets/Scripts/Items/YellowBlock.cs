using System;
using Zenject;

namespace ClickerGame
{
    public class YellowBlock : Item
    {
        Settings _settings;

        [Inject]
        public void Construct(Settings settings,
                            SignalBus signalBus)
        {
            base.Construct(settings, signalBus);
            _settings = settings;
        }

        [Serializable]
        public new class Settings : Item.Settings
        {
        }

        public new class Factory : Item.Factory
        {
            public Factory(Settings settings) : base(settings)
            {
            }
        }
    }
}