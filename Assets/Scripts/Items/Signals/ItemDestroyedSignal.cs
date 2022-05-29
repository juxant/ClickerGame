namespace ClickerGame.Items.Signals
{
    public struct ItemDestroyedSignal
    {
        public float Points { get; }

        public ItemDestroyedSignal(float points)
        {
            Points = points;
        }
    }
}