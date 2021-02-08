using SelectionSystem.Internal;
using UnityEngine;

namespace SelectionSystem.Base
{
    /// <summary>
    /// The Selectable class act as: 
    /// <para> - A base class from which custom classes can Inherit from. </para>
    /// When it's inherited all the logic must be written down.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Selectable : MonoBehaviour, ISelectable
    {
        public abstract bool isSelected
        {
            get;
        }

        protected virtual void OnDestroy()
        {
            Events.InvokeOnSelectableDestroyed(this);
        }

        public abstract void Select();
        public abstract void Deselect();
    }
}
