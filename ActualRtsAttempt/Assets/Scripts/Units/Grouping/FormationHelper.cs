using Assets.Scripts.Units.Grouping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationHelper : MonoBehaviour
{
    public Group group;
    public Dictionary<string, GroupFormation> PossibleFormations; //Possible formations, should not be edited directly these contain OFFSETS not the final coordinates
    public float Standard_offset = 2f; //TODO: const
    void Start()
    {
        if (group == null) group = transform.GetComponent<Group>();
        /* Cool double line protect middle formation thingy
         * List<(GameObject, Vector3)> Arrow = new List<(GameObject, Vector3)>();
        int leftUnits = 1;
        int rightUnits = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.gameObject.layer != 5)
            {
                if (i == 0) Arrow.Add((child.gameObject, new Vector3(0, 0, 0)));
                else
                {
                    int leftOrRight = i % 2;
                    if (leftOrRight == 0) // Left 
                    {
                        Vector3 standard_offset = (new Vector3(Standard_offset, 0, Standard_offset)) - Vector3.forward * leftUnits;
                        Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position -standard_offset));
                        leftUnits++;
                    }
                    else //right
                    {
                        Vector3 standard_offset = (new Vector3(Standard_offset, 0, Standard_offset)) - Vector3.forward * rightUnits;
                        Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position + standard_offset));
                        rightUnits++;
                    }
                }
            }
        }
        GroupFormation arrowForm = new GroupFormation();
        arrowForm.idealformation = Arrow;
        arrowForm.name = "Arrow";
         */
        List<(GameObject, Vector3)> Arrow = new List<(GameObject, Vector3)>();
        int leftUnits = 1;
        int rightUnits = 1;
        int actualChildCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer != 5)
                actualChildCount++;
        }
        int middlepoint = (int)(actualChildCount / 2f);
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.gameObject.layer != 5)
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
        GroupFormation arrowForm = new GroupFormation();
        arrowForm.idealformation = Arrow;
        arrowForm.name = "Arrow";
        PossibleFormations = new Dictionary<string, GroupFormation>();
        PossibleFormations.Add("Arrow", arrowForm);
        if(group.GroupUnits != null)
            SetFormation();
    }

    public void SetFormation()
    { 
        var positions = GetImperfectPos("Arrow", 0.05f);
        for (int i = 0; i < group.GroupUnits.Count; i++)
        {
            var curUnit = group.GroupUnits[i];
            var formationUnit = positions.Where(pos => pos.Item1.GetInstanceID().Equals(curUnit.UnitObject.GetInstanceID())).FirstOrDefault();
            curUnit.UnitAgent.destination += formationUnit.Item2;
        }
    }

    private List<(GameObject, Vector3)> GetImperfectPos(string formationName, float errorMargin = 0.1f)
    {
        var perfFormation = PossibleFormations["Arrow"];
        List<(GameObject, Vector3)> imperfectFormation = new List<(GameObject, Vector3)>();
        for (int i = 0; i < perfFormation.idealformation.Count; i++)
        {
            imperfectFormation.Add((perfFormation.idealformation[i].Item1, perfFormation.idealformation[i].Item2 * Random.Range(1f - (errorMargin / 2), 1f + (errorMargin / 2f))));
        }
        return imperfectFormation;
    }
}
