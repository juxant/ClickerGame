using ClickerGame.Items.Signals;
using System;
using UnityEngine;
using Zenject;

namespace ClickerGame.Items
{
    public class Target : Item
    {
        Settings _settings;

        [Inject]
        public void Construct(Settings settings,
                            SignalBus signalBus)
        {
            base.Construct(settings, signalBus);
            _settings = settings;
        }

        protected override void OnMouseDown()
        {
            if (--Life <= 0)
            {
                SignalBus.Fire(new ItemDestroyedSignal(_settings.PointsWhenIsClicked));
                SignalBus.Fire(new ItemToBeEnqueueSignal(typeof(Coin.Factory), _settings.AmountItemsToBeEnqueue));
                Pool.Despawn(this);
            }
        }

        [Serializable]
        public new class Settings : Item.Settings
        {
            [field: SerializeField] public int AmountItemsToBeEnqueue { get; private set; }
        }

        public new class Factory : Item.Factory
        {
            public Factory(Settings settings) : base(settings) { }
        }
    }
}