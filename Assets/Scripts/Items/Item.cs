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
            SignalBus.Subscribe<GameOverSignal>(Despawn);
        }

        protected void OnEnable()
        {
            StartTime = Time.realtimeSinceStartup;
        }

        protected virtual void Update()
        {
            if (Time.realtimeSinceStartup - StartTime > _settings.LifeTimeInSeconds)
            {
                Destroy(_settings.PointsWhenIsMissed);
            }
        }

        protected virtual void OnMouseDown()
        {
            if (--Life <= 0)
            {
                Destroy(_settings.PointsWhenIsClicked);
            }
        }

        private void OnDestroy()
        {
            SignalBus.Unsubscribe<GameOverSignal>(Despawn);
        }

        protected void Despawn()
        {
            if (gameObject.activeSelf)
            {
                Pool.Despawn(this);
            }           
        }

        protected void Destroy(float points)
        {
            SignalBus.Fire(new ItemDestroyedSignal(points));
            Despawn();
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