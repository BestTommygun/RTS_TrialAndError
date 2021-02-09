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
        private SelectionEvents _selectionEvents = new SelectionEvents();

        private SelectionHighlight _selectionHighlight;

        /// <summary>
        /// Access the selection events for this selectable instance. (Read Only)
        /// </summary>
        public SelectionEvents selectionEvents => _selectionEvents;

        /// <summary>
        /// Is this object selected?
        /// </summary>
        public bool isSelected { get; private set; } = false;

        private void Start()
        {
            if (hLSelection.TryGetComponent<SelectionHighlight>(out var sh))
            {
                _selectionHighlight = sh;
            }
            else
            {
                _selectionHighlight = GetComponentInChildren<SelectionHighlight>(true);
            }

            Deselect();
        }

        private void OnDisable()
        {
            onDestroy?.Invoke(this);
        }

        /// <summary>
        /// Select this object.
        /// </summary>
        public void Select()
        {
            isSelected = true;
            _selectionEvents.onSelection?.Invoke();

            if (_selectionHighlight)
            {
                _selectionHighlight.Activate();
                return;
            }
            
            hLSelection.SetActive(true);
        }
        /// <summary>
        /// Deselect this object.
        /// </summary>
        public void Deselect()
        {
            isSelected = false;
            _selectionEvents.onDeselection?.Invoke();

            if (_selectionHighlight)
            {
                _selectionHighlight.Deactivate();
                return;
            }

            hLSelection.SetActive(false);
        }
    }
}