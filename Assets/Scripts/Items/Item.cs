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


        public virtual void Construct(Settings settings)
        {
            _settings = settings;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            _pool.Despawn(this);
        }

        public virtual void Update()
        {
            transform.position -= transform.right * _settings.Speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _settings.LifeTime)
            {
                _pool.Despawn(this);
            }
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
            [field: SerializeField] public float Speed { get; private set; }
            [field: SerializeField] public float LifeTime { get; private set; }
        }      
    }
}