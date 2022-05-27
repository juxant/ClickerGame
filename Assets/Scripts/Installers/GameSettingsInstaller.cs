using System;
using UnityEngine;
using Zenject;

namespace ClickerGame
{
    [CreateAssetMenu(menuName = "ClickerGame/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public ItemSettings Item { get; private set; }
        [field: SerializeField] public GlobalInstaller.Settings GlobalInstaller { get; private set; }
        [field: SerializeField] public LevelBoundary.Settings LevelBoundary { get; private set; }


        [Serializable]
        public class ItemSettings
        {
            [field: SerializeField] public ItemSpawner.Settings Spawner { get; private set; }
            [field: SerializeField] public Coin.Settings Coin { get; private set; }
            [field: SerializeField] public BlueSphere.Settings BlueSphere { get; private set; }
            [field: SerializeField] public YellowBlock.Settings YellowBlock { get; private set; }
            [field: SerializeField] public RedBox.Settings RedBox { get; private set; }
            [field: SerializeField] public Shield.Settings Shield { get; private set; }
            [field: SerializeField] public Target.Settings Target { get; private set; }
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Item.Spawner);
            Container.BindInstance(Item.Coin);
            Container.BindInstance(Item.BlueSphere);
            Container.BindInstance(Item.YellowBlock);
            Container.BindInstance(Item.RedBox);
            Container.BindInstance(Item.Shield);
            Container.BindInstance(Item.Target);
            Container.BindInstance(GlobalInstaller);
            Container.BindInstance(LevelBoundary);
        }
    }
}