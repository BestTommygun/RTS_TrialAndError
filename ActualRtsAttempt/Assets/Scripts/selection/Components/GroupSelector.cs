using System;
using UnityEngine;
using SelectionSystem.Modules;
using System.Collections.Generic;

namespace SelectionSystem.Components
{
    /// <summary>
    /// The Selector act as: 
    /// <para> - A single Component ready-to-use so your custom class can use it's functionalities. (SelectionSystem.Component) <br/>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public sealed class GroupSelector : MonoBehaviour, ISelectable
    {
        internal static event Action<ISelectable> onDestroy;

        [SerializeField]
        private GameObject hLSelection;
        public SelectionHandler SelectionHandler;
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
            if(hLSelection != null)
            {
                if (hLSelection.TryGetComponent<SelectionHighlight>(out var sh))
                {
                    _selectionHighlight = sh;
                }
                else
                {
                    _selectionHighlight = GetComponentInChildren<SelectionHighlight>(true);
                }
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

            if (_selectionHighlight)
            {
                _selectionHighlight.Activate();
                return;
            }
            if(hLSelection != null)
                hLSelection.SetActive(true);

            //select all children aswell
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.layer != 5)
                {
                    SelectionHandler.currentSelection.Add(transform.GetChild(i).GetComponent<ISelectable>());
                }
            }
            SelectionHandler.currentSelection.Add(this);
        }
        /// <summary>
        /// Deselect this object.
        /// </summary>
        public void Deselect()
        {
            isSelected = false;

            if (_selectionHighlight)
            {
                _selectionHighlight.Deactivate();
                return;
            }
            if (hLSelection != null)
                hLSelection.SetActive(false);

            //deselect all children aswell
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.layer != 5)
                {
                    SelectionHandler.currentSelection.Remove(transform.GetChild(i).GetComponent<ISelectable>());
                }
            }
            SelectionHandler.currentSelection.Remove(this);
        }
    }
}