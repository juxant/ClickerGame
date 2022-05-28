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
            InstallSignals();

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

            Container.BindFactoryCustomInterface<Item, Coin.Factory, Item.Factory>()
                .To<Coin>()
                .FromPoolableMemoryPool<Coin, CoinPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.CoinPrefab)
                .UnderTransform(coins));

            Container.BindFactoryCustomInterface<Item, BlueSphere.Factory, Item.Factory>()
                .To<BlueSphere>()
                .FromPoolableMemoryPool<BlueSphere, BlueSpherePool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.BlueSpherePrefab)
                .UnderTransform(blueSphere));

            Container.BindFactoryCustomInterface<Item, YellowBlock.Factory, Item.Factory>()
                .To<YellowBlock>()
                .FromPoolableMemoryPool<YellowBlock, YellowBlockPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.YellowBlockPrefab)
                .UnderTransform(yellowBlock));

            Container.BindFactoryCustomInterface<Item, RedBox.Factory, Item.Factory>()
                .To<RedBox>()
                .FromPoolableMemoryPool<RedBox, RedBoxPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.RedBoxPrefab)
                .UnderTransform(redBox));

            Container.BindFactoryCustomInterface<Item, Shield.Factory, Item.Factory>()
                .To<Shield>()
                .FromPoolableMemoryPool<Shield, ShieldPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.ShieldPrefab)
                .UnderTransform(shield));

            Container.BindFactoryCustomInterface<Item, Target.Factory, Item.Factory>()
                .To<Target>()
                .FromPoolableMemoryPool<Target, TargetPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_settings.TargetPrefab)
                .UnderTransform(target));

            Container.Bind<LevelBoundary>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemSpawner>().AsSingle();
        }

        void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ItemDestroyedSignal>();
            Container.DeclareSignal<ItemToBeEnqueueSignal>();
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

        class MonoPoolableMemoryPoolCustom<TParam1, TValue> : MonoPoolableMemoryPool<TParam1, TValue> where TValue : Component, IPoolable<TParam1>
        {
            //disable gameObject.SetActive(true) in MonoPoolableMemoryPool Reinitialize method
            protected override void Reinitialize(TParam1 p1, TValue item)
            {
                item.OnSpawned(p1);
            }
        }
        
        class CoinPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Coin> { }

        class BlueSpherePool : MonoPoolableMemoryPoolCustom<IMemoryPool, BlueSphere> { }

        class YellowBlockPool : MonoPoolableMemoryPoolCustom<IMemoryPool, YellowBlock> { }

        class RedBoxPool : MonoPoolableMemoryPoolCustom<IMemoryPool, RedBox> { }

        class ShieldPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Shield> { }

        class TargetPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Target> { }
    }
}