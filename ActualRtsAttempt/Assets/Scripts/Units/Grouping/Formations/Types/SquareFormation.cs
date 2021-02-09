using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.Grouping.Formations
{
    [Formation("Square")]
    public class SquareFormation : IFormation
    {
        public List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform)
        {
            throw new NotImplementedException();
        }

        public List<(GameObject, Transform)> ApplyFormationLogicRelativeToParent(Transform ParentTransform)
        {
            throw new NotImplementedException();
        }
    }
}
