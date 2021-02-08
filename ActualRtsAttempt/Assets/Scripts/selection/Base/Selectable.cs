using UnityEngine;

namespace SelectionSystem.Base
{
    /// <summary>
    /// The Selectable class act as: 
    /// <para> A base class from which custom classes can Inherit from. </para>
    /// When it's inherited all the logic must be written down.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Selectable : MonoBehaviour, ISelectable
    {
        /// <inheritdoc/>
        public abstract bool isSelected
        {
            get;
        }

        /// <inheritdoc/>
        public abstract void Select();

        /// <inheritdoc/>
        public abstract void Deselect();
    }
}
