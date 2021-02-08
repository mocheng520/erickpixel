using System;

namespace ResourcesSystem.Data
{
    [Serializable]
    public struct ResourceCost
    {
        public GameResource type;
        public int amount;
    }
}