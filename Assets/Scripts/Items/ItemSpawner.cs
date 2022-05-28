using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    public class ItemSpawner : ITickable, IInitializable, IDisposable
    {
        readonly Queue<Item> _itemsToSpawn;
        readonly List<Item.Factory> _itemFactories;
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        private readonly LevelBoundary _levelBoundary;

        float _desiredNumItems;
        float _delayBetweenSpawns;
        int _itemCount;
        float _lastSpawnTime;

        public ItemSpawner(List<Item.Factory> itemFactories,
                        Settings settings,
                        SignalBus signalBus,
                        LevelBoundary levelBoundary)
        {
            _itemsToSpawn = new Queue<Item>();
            _itemFactories = itemFactories;
            _settings = settings;
            _signalBus = signalBus;
            _levelBoundary = levelBoundary;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ItemDestroyedSignal>(OnItemDestroyed);
            _signalBus.Subscribe<ItemToBeEnqueueSignal>(EnqueueItems);
        }

        void OnItemDestroyed(ItemDestroyedSignal _)
        {
            _itemCount--;
        }

        public void Tick()
        {
            if (_itemsToSpawn.Count < _settings.MaxItemsToBeEnqueue)
            {
                EnqueueRandomItemByChance();
                Debug.Log(_settings.MaxItemsInScreen);
            }

            if (_itemCount < _settings.MaxItemsInScreen
                && Time.realtimeSinceStartup - _lastSpawnTime > _delayBetweenSpawns)
            {
                SpawnItem();              
            }
        }

        void EnqueueItems(ItemToBeEnqueueSignal itemToBeEnqueue)
        {
            var itemFactory = _itemFactories.FirstOrDefault(x => x.GetType() == itemToBeEnqueue.Type);
            _itemsToSpawn.Clear();

            for (int i = 0; i < itemToBeEnqueue.Count; i++)
            {
                _itemsToSpawn.Enqueue(itemFactory.Create());
            }
        }

        void EnqueueRandomItemByChance()
        {
            var itemFactory = SelectRandomItemByChance(_itemFactories);
            var item = itemFactory.Create();
            _itemsToSpawn.Enqueue(item);
        }

        void SpawnItem()
        {
            if (!_itemsToSpawn.Any())
            {
                return;
            }

            var item = _itemsToSpawn.Dequeue();
            item.transform.position = GetRandomPosition();
            item.gameObject.SetActive(true);
            _itemCount++;

            _lastSpawnTime = Time.realtimeSinceStartup;
            _delayBetweenSpawns = UnityEngine.Random.Range(_settings.MinSecondsBetweenSpawns, _settings.MaxSecondsBetweenSpawns);
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

            var poolSize = items.Sum(x => x.Settings.SpawnChance);
            var randomNumber = UnityEngine.Random.Range(0f, poolSize);

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

        public void Dispose()
        {
            _signalBus.Unsubscribe<ItemDestroyedSignal>(OnItemDestroyed);
            _signalBus.Unsubscribe<ItemToBeEnqueueSignal>(EnqueueItems);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float MaxItemsInScreen { get; private set; }
            [field: SerializeField] public float MaxItemsToBeEnqueue { get; private set; }

            [field: SerializeField] public float MinSecondsBetweenSpawns { get; private set; } = 0.5f;
            [field: SerializeField] public float MaxSecondsBetweenSpawns { get; private set; } = 2f;
        }
    }   
}