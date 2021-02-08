using UnityEngine;

namespace SelectionSystem.Modules.MeshGeneration
{
    /// <summary>
    /// Handles the creation of the Mesh to the Mesh Collider compoenent that detects which selectable objects are inside of the 2d rectangle box (Screen). 
    /// <para>This is Physics based, so a Rigidbody is required even if not in use. <br/><br/>
    /// <remarks>Hint: To avoid performance issues in case a Rigidbody is not directly being used, the recommended is to mark it as Kinematic.</remarks>
    /// </para>
    /// </summary>
    public static class SelectionBoxGenerator
    {
        /// <summary>
        /// Generate the Mesh for the multi-selection of selectables. The Mesh will be applied to the <see cref="MeshCollider"/> attached. It will be automatically set to Convex and Trigger.
        /// </summary>
        /// <param name="initialPosition">The first position when the selection box has started. Usually the point of the first click before dragging the cursor.</param>
        /// <param name="finalPosition">The last position for the selection box. Usually this is the actual position of the cursor.</param>
        /// <param name="mainCamera">The camera from which the rays will be cast, so the box shape will be made.</param>
        /// <param name="attachedMeshCollider">The <see cref="MeshCollider"/> attached to the <seealso cref="GameObject"/>. 
        /// It must have one, so instead of create and destroy each time a selection box is made, it just need to be enabled and disabled. </param>
        /// <returns>True if a selection box where generated. Which indicates that the current <seealso cref="MeshCollider"/> is enabled. 
        /// Otherwise, if some of the Corners fails on hit something, then it will return false and no Mesh will be generated. 
        /// (The <seealso cref="MeshCollider"/> will be disabled automatically in this case). 
        /// <para>This allows the current <see cref="MonoBehaviour"/> itself to handles the disable of the <seealso cref="MeshCollider"/>. <br/> 
        /// Since it needs to be active for a short period of time so the OnTriggerEnter callback can correctly triggers all the Units inside the collider generated. </para> 
        /// </returns>
        public static bool Generate(Vector3 initialPosition, Vector3 finalPosition, Camera mainCamera, ref MeshCollider attachedMeshCollider)
        {
            if (Mathf.Abs(initialPosition.x - finalPosition.x) < Constants.MIN_ACCEPTABLE_VERTEX_DISTANCE ||
                Mathf.Abs(initialPosition.y - finalPosition.y) < Constants.MIN_ACCEPTABLE_VERTEX_DISTANCE)
            {
                return false;   // Returns out of the function if the points are too close from each other.
            }

            Vector3[] verticies = new Vector3[4];
            Vector3[] meshBorderLines = new Vector3[4];
            Vector2[] corners = MeshBounds.GetBoundaries(initialPosition, finalPosition);

            int i = 0;
            foreach (Vector2 corner in corners)
            {
                Ray ray = mainCamera.ScreenPointToRay(corner); // Used to read the origin point from camera view.

                var screenPoint = new Vector3(corner.x, corner.y, mainCamera.farClipPlane);
                verticies[i] = mainCamera.ScreenToWorldPoint(screenPoint);
                meshBorderLines[i] = ray.origin - verticies[i];

                i++;
            }

            Mesh selectionBoxMesh = MeshGenerator.GenerateMesh(verticies, meshBorderLines);

            attachedMeshCollider.sharedMesh = selectionBoxMesh;
            attachedMeshCollider.enabled = true;
            attachedMeshCollider.convex = true;
            attachedMeshCollider.isTrigger = true;

            return attachedMeshCollider.enabled;
        }
    }
}
