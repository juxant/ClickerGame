using System;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    public abstract class Item : MonoBehaviour, IPoolable<IMemoryPool>
    {
        float _startTime;

        IMemoryPool _pool;
        Settings _settings;
        SignalBus _signalBus;

        public virtual void Construct(Settings settings,
                                    SignalBus signalBus)
        {
            _settings = settings;
            _signalBus = signalBus;
        }

        public void Destroy()
        {
            _signalBus.Fire<ItemDestroyedSignal>();
            _pool.Despawn(this);
        }

        public virtual void OnMouseDown()
        {
            Destroy();
        }

        public virtual void Update()
        {
            //transform.position -= transform.right * _settings.Speed * Time.deltaTime;

            //if (Time.realtimeSinceStartup - _startTime > _settings.LifeTime)
            //{
            //    Destroy();
            //}
        }

        public virtual void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _startTime = Time.realtimeSinceStartup;
        }

        public virtual void OnDespawned()
        {
            _pool = null;
        }

        [Serializable]
        public abstract class Settings
        {
            [field: SerializeField] public float PointsEarn { get; private set; }
            [field: SerializeField] public float PointsLose { get; private set; }
            [field: SerializeField] public float Life { get; private set; }
            [field: SerializeField] public float SpawnChance { get; private set; }
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