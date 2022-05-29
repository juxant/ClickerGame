using System;

namespace ClickerGame.Items.Signals
{
    public class ItemToBeEnqueueSignal
    {
        public Type Type { get; }
        public int Count { get; }

        public ItemToBeEnqueueSignal(Type type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}