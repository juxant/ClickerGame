using System;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    public class GlobalInstaller : MonoInstaller
    {
        [Inject] readonly Settings _settings;

        public override void InstallBindings()
        {
            var items = new GameObject("Items").transform;

            var coins = new GameObject("Coins").transform;
            coins.SetParent(items.transform);

            var blueSphere = new GameObject("BlueSphere").transform;
            blueSphere.SetParent(items.transform);

            var yellowBlock = new GameObject("YellowBlock").transform;
            yellowBlock.SetParent(items.transform);

            var redBox = new GameObject("RedBox").transform;
            redBox.SetParent(items.transform);

            var shield = new GameObject("Shield").transform;
            shield.SetParent(items.transform);

            var target = new GameObject("Target").transform;
            target.SetParent(items.transform);

            Container.BindFactory<Coin, Coin.Factory>()
                .FromPoolableMemoryPool<Coin, CoinPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.CoinPrefab)
                    .UnderTransform(coins));

            Container.BindFactory<BlueSphere, BlueSphere.Factory>()
                .FromPoolableMemoryPool<BlueSphere, BlueSpherePool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.BlueSpherePrefab)
                    .UnderTransform(blueSphere));

            Container.BindFactory<YellowBlock, YellowBlock.Factory>()
                .FromPoolableMemoryPool<YellowBlock, YellowBlockPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.YellowBlockPrefab)
                    .UnderTransform(yellowBlock));

            Container.BindFactory<RedBox, RedBox.Factory>()
                .FromPoolableMemoryPool<RedBox, RedBoxPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.RedBoxPrefab)
                    .UnderTransform(redBox));

            Container.BindFactory<Shield, Shield.Factory>()
                .FromPoolableMemoryPool<Shield, ShieldPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.ShieldPrefab)
                    .UnderTransform(shield));

            Container.BindFactory<Target, Target.Factory>()
                .FromPoolableMemoryPool<Target, TargetPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_settings.TargetPrefab)
                    .UnderTransform(target));
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public GameObject CoinPrefab { get; private set; }
            [field: SerializeField] public GameObject BlueSpherePrefab { get; private set; }
            [field: SerializeField] public GameObject YellowBlockPrefab { get; private set; }
            [field: SerializeField] public GameObject RedBoxPrefab { get; private set; }
            [field: SerializeField] public GameObject ShieldPrefab { get; private set; }
            [field: SerializeField] public GameObject TargetPrefab { get; private set; }
        }

        class CoinPool : MonoPoolableMemoryPool<IMemoryPool, Coin> { }
        class BlueSpherePool : MonoPoolableMemoryPool<IMemoryPool, BlueSphere> { }
        class YellowBlockPool : MonoPoolableMemoryPool<IMemoryPool, YellowBlock> { }
        class RedBoxPool : MonoPoolableMemoryPool<IMemoryPool, RedBox> { }
        class ShieldPool : MonoPoolableMemoryPool<IMemoryPool, Shield> { }
        class TargetPool : MonoPoolableMemoryPool<IMemoryPool, Target> { }
    }
}