using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    public class ItemSpawner : ITickable, IInitializable
    {
        readonly List<Item.Factory> _items;
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        private readonly LevelBoundary _levelBoundary;

        float _desiredNumEnemies;
        int _enemyCount;
        float _lastSpawnTime;

        public ItemSpawner(List<Item.Factory> items,
                        Settings settings,
                        SignalBus signalBus,
                        LevelBoundary levelBoundary)
        {
            _items = items;
            _settings = settings;
            _signalBus = signalBus;
            _levelBoundary = levelBoundary;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ItemDestroyedSignal>(OnItemDestroyed);
        }

        void OnItemDestroyed()
        {
            _enemyCount--;
        }

        public void Tick()
        {
            _desiredNumEnemies += _settings.NumItemsIncreaseRate * Time.deltaTime;

            if (_enemyCount < 5)
            {
                SpawnItem();
                _enemyCount++;
            }

        }

        void SpawnItem()
        {
            var itemFactory = SelectRandomItemByChance(_items);
            var item = itemFactory.Create();
            item.transform.position = GetRandomPosition();

            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        Vector3 GetRandomPosition()
        {
            var randomX = UnityEngine.Random.Range(_levelBoundary.Left, _levelBoundary.Right);
            var randomY = UnityEngine.Random.Range(_levelBoundary.Bottom, _levelBoundary.Top);
            
            return new Vector3(randomX, randomY);
        }

        Item.Factory SelectRandomItemByChance(List<Item.Factory> items)
        {
            Item.Factory itemSelected = null;

            // Calculate the summa of all portions.
            var poolSize = items.Sum(x => x.Settings.SpawnChance);

            // Get a random integer from 0 to PoolSize.
            var randomNumber = UnityEngine.Random.Range(0f, poolSize) + 1f;

            // Detect the item, which corresponds to current random number.
            var accumulatedProbability = 0f;
            for (var i = 0; i < items.Count; i++)
            {
                accumulatedProbability += items[i].Settings.SpawnChance;
                if (randomNumber <= accumulatedProbability)
                {
                    itemSelected = items[i];
                    break;
                }                  
            }

            return itemSelected;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float NumItemsIncreaseRate { get; private set; }
            [field: SerializeField] public float MaxItemsInScreen { get; private set; }

            [field: SerializeField] public float MinTimeBetweenSpawns { get; private set; } = 0.5f;
            [field: SerializeField] public float MaxTimeBetweenSpawns { get; private set; } = 2f;
        }
    }   
}