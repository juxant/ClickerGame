using System;
using Zenject;

namespace ClickerGame.Items
{
    public class Shield : Item
    {
        [Inject]
        public void Construct(Settings settings, SignalBus signalBus)
        {
            base.Construct(settings, signalBus);
        }

        [Serializable]
        public new class Settings : Item.Settings { }

        public new class Factory : Item.Factory
        {
            public Factory(Settings settings) : base(settings) { }
        }
    }
}