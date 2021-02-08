using SelectionSystem.Components;

namespace SelectionSystem
{
    /// <summary>
    /// This interface makes the <see cref="SelectionHandler"/> recognizes any object as a selectable one. <br/>
    /// </summary>
    public interface ISelectable
    {
        /// <summary> Is the object selected? </summary>
        bool isSelected { get; }

        /// <summary>
        /// Select this object.
        /// </summary>
        void Select();

        /// <summary>
        /// Deselect this object.
        /// </summary>
        void Deselect();
    }
}
