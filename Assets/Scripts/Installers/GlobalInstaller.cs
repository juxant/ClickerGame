using ClickerGame.Game;
using ClickerGame.Game.Signals;
using ClickerGame.Items.Signals;
using Zenject;

namespace ClickerGame.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSignals();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }

        void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ItemDestroyedSignal>();
            Container.DeclareSignal<ItemToBeEnqueueSignal>();
            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<GameOverSignal>();
        }
    }
}