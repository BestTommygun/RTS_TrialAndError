using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Grouping.Formations
{
    public interface IFormation
    {
        /// <summary>
        /// Applies formation logic to get whatever the formation should be for your unit, does not take worldspace or rotation into account,
        /// </summary>
        /// <param name="ParentTransform">the parent squad object</param>
        /// <returns>the formation</returns>
        List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform);
        /// <summary>
        /// Applies formation logic to get what positions your units should be in, Formation coordinates take worldspace and rotation into account.
        /// </summary>
        /// <param name="ParentTransform">the parent squad object</param>
        /// <returns>the used formation in worldspace</returns>
        List<(GameObject, Transform)> ApplyFormationLogicRelativeToParent(Transform ParentTransform);
    }
}
