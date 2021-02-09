using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.Grouping.Formations
{
    [Formation("Arrow")]
    public class ArrowFormation : IFormation
    {
        public List<(GameObject, Vector3)> ApplyFormationLogic(Transform ParentTransform)
        {
            if (ParentTransform.childCount <= 0) return null;
            float Standard_offset = ParentTransform.GetChild(0).GetComponent<NavMeshAgent>().radius * 3f;

            List<(GameObject, Vector3)> Arrow = new List<(GameObject, Vector3)>();
            int leftUnits = 1;
            int rightUnits = 1;
            int actualChildCount = 0;
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                if (ParentTransform.GetChild(i).gameObject.layer != 5)
                    actualChildCount++;
            }
            int middlepoint = (int)(actualChildCount / 2f);
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                var child = ParentTransform.GetChild(i);
                if (child.gameObject.layer != 5)
                {
                    if (i == 0) Arrow.Add((child.gameObject, new Vector3(0, 0, 0)));
                    else
                    {
                        bool IsLeftRow = i <= middlepoint;
                        if (IsLeftRow) // Left 
                        {
                            Vector3 standard_offset = new Vector3(-Standard_offset, 0, -Standard_offset) * leftUnits;
                            Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position + standard_offset));
                            leftUnits++;
                        }
                        else //right
                        {
                            Vector3 standard_offset = new Vector3(Standard_offset, 0, -Standard_offset) * rightUnits;
                            Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position + standard_offset));
                            rightUnits++;
                        }
                    }
                }
            }
            return Arrow;
        }

        public List<(GameObject, Vector3)> ApplyFormationLogicRelativeToParent(Transform ParentTransform, Transform FormationAnchor)
        {
            if (ParentTransform.childCount <= 0) return null;
            float Standard_offset = ParentTransform.GetChild(0).GetComponent<NavMeshAgent>().radius * 3f;

            List<(GameObject, Vector3)> Arrow = new List<(GameObject, Vector3)>();
            int leftUnits = 1;
            int rightUnits = 1;
            int actualChildCount = 0;
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                if (ParentTransform.GetChild(i).gameObject.layer != 5)
                    actualChildCount++;
            }
            int middlepoint = (int)(actualChildCount / 2f);
            for (int i = 0; i < ParentTransform.childCount; i++)
            {
                var child = ParentTransform.GetChild(i);
                if (child.gameObject.layer != 5)
                {
                    if (i == 0) Arrow.Add((child.gameObject, new Vector3(0, 0, 0)));
                    else
                    {
                        bool IsLeftRow = i <= middlepoint;
                        if (IsLeftRow) // Left 
                        {
                            Vector3 standard_offset = new Vector3(-Standard_offset, 0, -Standard_offset) * leftUnits;
                            Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position + standard_offset));
                            leftUnits++;
                        }
                        else //right
                        {
                            Vector3 standard_offset = new Vector3(Standard_offset, 0, -Standard_offset) * rightUnits;
                            Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position + standard_offset));
                            rightUnits++;
                        }
                    }
                }
            }
            return Arrow;
        }
    }
}
