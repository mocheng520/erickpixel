using System;
using UnityEngine;
using SelectionSystem.Modules;

namespace SelectionSystem.Components
{
    /// <summary>
    /// The Selector act as: 
    /// <para> - A single Component ready-to-use so your custom class can use it's functionalities. (SelectionSystem.Component) <br/>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public sealed class Selector : MonoBehaviour, ISelectable
    {
        internal static event Action<ISelectable> onDestroy;

        [SerializeField]
        private GameObject hLSelection;

        [Space]
        [SerializeField]
        private SelectionEvents selectionEvents = new SelectionEvents();

        private ISelectionHighlight hlSelectionComponent;

        /// <summary>
        /// Access the selection events for this selectable instance. (Read Only)
        /// </summary>
        public SelectionEvents SelectionEvents { get => selectionEvents; }

        /// <summary>
        /// Is this object selected?
        /// </summary>
        public bool isSelected { get; private set; } = false;

        private void Start()
        {
            if (hLSelection.TryGetComponent(out ISelectionHighlight selectionHighlight))
            {
                hlSelectionComponent = selectionHighlight;
                Deselect();
            }
            else
            {
                hLSelection.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke(this);
        }

        /// <summary>
        /// Select this object.
        /// </summary>
        public void Select()
        {
            isSelected = true;
            selectionEvents.onSelection.Invoke();

            if (hlSelectionComponent != null)
                hlSelectionComponent.Activate();
            else
                hLSelection.SetActive(true);
        }

        /// <summary>
        /// Deselect this object.
        /// </summary>
        public void Deselect()
        {
            isSelected = false;
            selectionEvents.onDeselection.Invoke();

            if (hlSelectionComponent != null)
                hlSelectionComponent.Deactivate();
            else
                hLSelection.SetActive(false);
        }
    }
}
