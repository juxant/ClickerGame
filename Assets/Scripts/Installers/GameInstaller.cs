using ClickerGame.Game;
using ClickerGame.Items;
using ClickerGame.Scenes;
using UnityEngine;
using Zenject;

namespace ClickerGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] readonly GameController _gameController;
        [Inject] readonly GameController.Settings _gameControllerSettings;
        [Inject] readonly GlobalSettings _globalSettings;

        public override void InstallBindings()
        {
            var gameSettingsInstaller = GameSettingsInstaller.InstallFromResource($"Installers/{_gameController?.DifficultyName ?? _gameControllerSettings.DefaultDifficulty}", Container);
            var items = new GameObject(nameof(Item)).transform;

            var coins = new GameObject(nameof(Coin)).transform;
            coins.SetParent(items.transform);

            var blueSphere = new GameObject(nameof(Sphere)).transform;
            blueSphere.SetParent(items.transform);

            var yellowBlock = new GameObject(nameof(Block)).transform;
            yellowBlock.SetParent(items.transform);

            var redBox = new GameObject(nameof(Box)).transform;
            redBox.SetParent(items.transform);

            var shield = new GameObject(nameof(Shield)).transform;
            shield.SetParent(items.transform);

            var target = new GameObject(nameof(Target)).transform;
            target.SetParent(items.transform);

            Container.BindFactoryCustomInterface<Item, Coin.Factory, Item.Factory>()
                .To<Coin>()
                .FromPoolableMemoryPool<Coin, CoinPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.CoinPrefab)
                .UnderTransform(coins));

            Container.BindFactoryCustomInterface<Item, Sphere.Factory, Item.Factory>()
                .To<Sphere>()
                .FromPoolableMemoryPool<Sphere, SpherePool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.SpherePrefab)
                .UnderTransform(blueSphere));

            Container.BindFactoryCustomInterface<Item, Block.Factory, Item.Factory>()
                .To<Block>()
                .FromPoolableMemoryPool<Block, BlockPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.BlockPrefab)
                .UnderTransform(yellowBlock));

            Container.BindFactoryCustomInterface<Item, Box.Factory, Item.Factory>()
                .To<Box>()
                .FromPoolableMemoryPool<Box, BoxPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.BoxPrefab)
                .UnderTransform(redBox));

            Container.BindFactoryCustomInterface<Item, Shield.Factory, Item.Factory>()
                .To<Shield>()
                .FromPoolableMemoryPool<Shield, ShieldPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.ShieldPrefab)
                .UnderTransform(shield));

            Container.BindFactoryCustomInterface<Item, Target.Factory, Item.Factory>()
                .To<Target>()
                .FromPoolableMemoryPool<Target, TargetPool>(poolBinder => poolBinder
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_globalSettings.TargetPrefab)
                .UnderTransform(target));

            Container.Bind<LevelBoundary>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemSpawner>().AsSingle();
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

        class SpherePool : MonoPoolableMemoryPoolCustom<IMemoryPool, Sphere> { }

        class BlockPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Block> { }

        class BoxPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Box> { }

        class ShieldPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Shield> { }

        class TargetPool : MonoPoolableMemoryPoolCustom<IMemoryPool, Target> { }
    }
}