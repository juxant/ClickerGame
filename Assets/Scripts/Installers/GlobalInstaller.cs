using Zenject;

namespace ClickerGame
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