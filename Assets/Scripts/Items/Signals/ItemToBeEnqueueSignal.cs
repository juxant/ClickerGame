using System;

namespace ClickerGame
{
    public struct ItemToBeEnqueueSignal
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