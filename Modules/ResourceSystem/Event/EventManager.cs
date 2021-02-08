using System;
using ResourcesSystem;

namespace ResourceSystem.Event
{
    public static class EventManager
    {
        public static event Action<ResourcesManager> OnResourceAmountChanges;

        public static void NotifyResourceAmountChanges(ResourcesManager notifier)
        {
            OnResourceAmountChanges(notifier);
        }
    }
}
