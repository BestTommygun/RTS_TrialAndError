using Assets.Scripts.Units.Grouping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationHelper : MonoBehaviour
{
    public Group group;
    public Dictionary<string, GroupFormation> PossibleFormations; //Possible formations, should not be edited directly these contain OFFSETS not the final coordinates
    public float Standard_offset = 0.5f; //TODO: const
    void Start()
    {
        if (group == null) group = transform.GetComponent<Group>();
        List<(GameObject, Vector3)> Arrow = new List<(GameObject, Vector3)>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.gameObject.layer != 5)
            {
                if (i == 0) Arrow.Add((child.gameObject, child.transform.position));
                else
                {
                    int leftOrRight = i % 2;
                    if (leftOrRight == 0) // Left 
                    {
                        Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position - new Vector3(-Standard_offset, 0, -Standard_offset) - Vector3.forward));
                    }
                    else //right
                    {
                        Arrow.Add((child.gameObject, Arrow[0].Item1.transform.position - new Vector3(Standard_offset, 0, Standard_offset) - Vector3.forward));
                    }
                }
            }
        }
        GroupFormation arrowForm = new GroupFormation();
        arrowForm.idealformation = Arrow;
        arrowForm.name = "Arrow";
        PossibleFormations = new Dictionary<string, GroupFormation>();
        PossibleFormations.Add("Arrow", arrowForm);
        SetFormation();
    }

    public void SetFormation()
    { 
        var positions = GetImperfectPos("Arrow");
        for (int i = 0; i < group.GroupUnits.Count; i++)
        {
            var curUnit = group.GroupUnits[i];
            var formationUnit = positions.Where(pos => pos.Item1.GetInstanceID().Equals(curUnit.UnitObject.GetInstanceID())).FirstOrDefault();
            curUnit.UnitAgent.destination += formationUnit.Item2;
        }
    }

    private List<(GameObject, Vector3)> GetImperfectPos(string formationName)
    {
        var perfFormation = PossibleFormations["Arrow"];
        List<(GameObject, Vector3)> imperfectFormation = new List<(GameObject, Vector3)>();
        for (int i = 0; i < perfFormation.idealformation.Count; i++)
        {
            imperfectFormation.Add((perfFormation.idealformation[i].Item1, perfFormation.idealformation[i].Item2 * Random.Range(0.95f, 1f)));
        }
        return imperfectFormation;
    }
}
