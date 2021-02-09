using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Grouping.Formations
{
    [Formation("Wedge")]
    public class WedgeFormation : IFormation
    {
        public List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform)
        {
            throw new NotImplementedException();
        }

        public List<(GameObject, Vector3)> ApplyFormationLogicRelativeToParent(Transform ParentTransform, Transform FormationAnchor)
        {
            throw new NotImplementedException();
        }
    }
}
