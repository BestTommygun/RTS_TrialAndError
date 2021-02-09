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
        public List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform)
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
                    if (i % (unitsPerRank) == 0) curRank++; //TODO: unitsPerRank + 1
                    int rankIndex = i % unitsPerRank;
                    Vector3 unitOffset = new Vector3((rankIndex - xoffset) * Standard_offset, 0, curRank * Standard_offset);
                    Column.Add((child.gameObject, unitOffset));
                }
            }
            return Column;
        }

        public List<(GameObject, Vector3)> ApplyFormationLogicRelativeToParent(Transform ParentTransform, Transform FormationAnchor)
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

            Group group = ParentTransform.GetComponent<Group>();
            group.GroupForward = FormationAnchor.transform.forward;
            Vector3 AnchorRightOffset = FormationAnchor.right * (int)(unitsPerRank / 2.0) * Standard_offset;
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                var child = ParentTransform.GetChild(i);
                if (child.gameObject.layer != 5)
                {
                    var mod = i % unitsPerRank;
                    if (i != 0 && i % (unitsPerRank) == 0) curRank++; //TODO: unitsPerRank + 1
                    int rankIndex = i % unitsPerRank;

                    Vector3 unitOffset = FormationAnchor.transform.right * Standard_offset * rankIndex + (-FormationAnchor.transform.forward * curRank);
                    unitOffset -= AnchorRightOffset; //The anchor is the top left of the formation, we want it to be in the middle, thus this offset is needed.
                    Column.Add((child.gameObject, unitOffset));
                }
            }
            return Column;
        }
    }
}
