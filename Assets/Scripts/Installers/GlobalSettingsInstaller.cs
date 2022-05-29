using ClickerGame.Game;
using ClickerGame.Scenes;
using System;
using UnityEngine;
using Zenject;

namespace ClickerGame.Installers
{
    [CreateAssetMenu(menuName = "ClickerGame/Global Settings")]
    public class GlobalSettingsInstaller : ScriptableObjectInstaller<GlobalSettingsInstaller>
    {
        [field: SerializeField] public GlobalSettings GlobalSettings { get; private set; }
        [field: SerializeField] public GameController.Settings GameController { get; private set; }
        [field: SerializeField] public LevelBoundary.Settings LevelBoundary { get; private set; }

        public override void InstallBindings()
        {
            Container.BindInstance(GlobalSettings);
            Container.BindInstance(GameController);
            Container.BindInstance(LevelBoundary);
        }
    }

    [Serializable]
    public class GlobalSettings
    {
        [field: SerializeField] public GameObject CoinPrefab { get; private set; }
        [field: SerializeField] public GameObject SpherePrefab { get; private set; }
        [field: SerializeField] public GameObject BlockPrefab { get; private set; }
        [field: SerializeField] public GameObject BoxPrefab { get; private set; }
        [field: SerializeField] public GameObject ShieldPrefab { get; private set; }
        [field: SerializeField] public GameObject TargetPrefab { get; private set; }
    }
}