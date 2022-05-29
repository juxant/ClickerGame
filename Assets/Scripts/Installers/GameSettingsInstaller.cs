using ClickerGame.Items;
using System;
using UnityEngine;
using Zenject;

namespace ClickerGame.Installers
{
    [CreateAssetMenu(menuName = "ClickerGame/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public ItemSpawner.Settings ItemSpawner { get; private set; }
        [field: SerializeField] public ItemSettings Item { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInstance(ItemSpawner);
            Container.BindInstance(Item.Coin);
            Container.BindInstance(Item.Sphere);
            Container.BindInstance(Item.Block);
            Container.BindInstance(Item.Box);
            Container.BindInstance(Item.Shield);
            Container.BindInstance(Item.Target);
        }

        [Serializable]
        public class ItemSettings
        {
            [field: SerializeField] public Coin.Settings Coin { get; private set; }
            [field: SerializeField] public Sphere.Settings Sphere { get; private set; }
            [field: SerializeField] public Block.Settings Block { get; private set; }
            [field: SerializeField] public Box.Settings Box { get; private set; }
            [field: SerializeField] public Shield.Settings Shield { get; private set; }
            [field: SerializeField] public Target.Settings Target { get; private set; }
        }
    }
}