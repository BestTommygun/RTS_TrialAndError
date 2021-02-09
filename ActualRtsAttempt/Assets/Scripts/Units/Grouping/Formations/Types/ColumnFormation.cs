using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.Grouping.Formations
{
    [Formation("Column")]
    public class ColumnFormation : IFormation
    {
        public List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform) //need a small touch up xD
        {
            if (ParentTransform.childCount <= 0) return null;
            List<(GameObject, Vector3)> Column = new List<(GameObject, Vector3)>();
            float Standard_offset = ParentTransform.GetChild(0).GetComponent<NavMeshAgent>().radius * 3f;
            int actualChildCount = 0;
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                if (ParentTransform.GetChild(i).gameObject.layer != 5)
                    actualChildCount++;
            }

            int ranksdeep = 3; //TODO: make dynamic pls
            int unitsPerRank = actualChildCount / ranksdeep;
            int curRank = 0;
            int xoffset = unitsPerRank / 2;
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                var child = ParentTransform.GetChild(i);
                if (child.gameObject.layer != 5)
                {
                    if (i == 0) Column.Add((child.gameObject, new Vector3(0, 0, 0)));
                    else
                    {
                        if (i % (unitsPerRank + 1) == 0) curRank++;
                        int rankIndex = i % unitsPerRank;
                        Vector3 unitOffset = new Vector3((rankIndex - xoffset) * Standard_offset, 0, curRank * Standard_offset);
                        Column.Add((child.gameObject, unitOffset));
                    }
                }
            }
            return Column;
        }

        public List<(GameObject, Transform)> ApplyFormationLogicRelativeToParent(Transform ParentTransform)
        {
            throw new NotImplementedException();
        }
    }
}
