using System;

namespace SelectionSystem.Internal
{
    internal static class Events
    {
        internal static event Action<ISelectable> OnSelectableDestroyed;

        internal static void InvokeOnSelectableDestroyed(ISelectable selectable)
        {
            OnSelectableDestroyed(selectable);
        }
    }
}
