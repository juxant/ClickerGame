using System;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    public abstract class Item : MonoBehaviour, IPoolable<IMemoryPool>
    {
        Settings _settings;

        protected float StartTime { get; set; }
        protected float Life { get; set; }
        protected IMemoryPool Pool { get; set; }       
        protected SignalBus SignalBus { get; private set; }

        protected virtual void Construct(Settings settings,
                                    SignalBus signalBus)
        {
            _settings = settings;
            SignalBus = signalBus;
        }

        protected void Destroy(float points)
        {
            SignalBus.Fire(new ItemDestroyedSignal(points));
            Pool.Despawn(this);
        }

        protected virtual void OnMouseDown()
        {
            if (--Life <= 0)
            {
                Destroy(_settings.PointsWhenIsClicked);
            }           
        }

        protected virtual void Update()
        {
            if (Time.realtimeSinceStartup - StartTime > _settings.LifeTimeInSeconds)
            {
                Destroy(_settings.PointsWhenIsMissed);
            }
        }

        private void OnEnable()
        {
            StartTime = Time.realtimeSinceStartup;
        }

        public virtual void OnSpawned(IMemoryPool pool)
        {
            Pool = pool;            
            Life = _settings.Life;
        }

        public virtual void OnDespawned()
        {
            Pool = null;
        }

        [Serializable]
        public abstract class Settings
        {
            [field: SerializeField] public float PointsWhenIsClicked { get; private set; }
            [field: SerializeField] public float PointsWhenIsMissed { get; private set; }
            [field: SerializeField] public float Life { get; private set; }
            [field: SerializeField] public float SpawnChance { get; private set; }
            [field: SerializeField] public float LifeTimeInSeconds { get; private set; }
        }

        public abstract class Factory : PlaceholderFactory<Item>, IFactory<Item>
        {
            public Settings Settings { get; }

            public Factory(Settings settings)
            {
                Settings = settings;
            }
        }
    }
}