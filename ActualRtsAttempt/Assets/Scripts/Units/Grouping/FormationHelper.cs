using Assets.Scripts.Units.Grouping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationHelper : MonoBehaviour
{
    public List<(GameObject, UnitMovement)> childMovements;
    public Dictionary<string, GroupFormation> PossibleFormations;
    public float Standard_offset = 0.5f; //TODO: const
    void Start()
    {
        List<(GameObject, SingleUnit)> Arrow = new List<(GameObject, SingleUnit)>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.gameObject.layer != 5)
            {
                if (i == 0) Arrow.Add((child.gameObject, new SingleUnit(child.transform.position)));
                else
                {
                    int leftOrRight = i % 2;
                    if (leftOrRight == 0) // Left 
                    {
                        Arrow.Add((child.gameObject, new SingleUnit(Arrow[0].Item1.transform.position - new Vector3(-Standard_offset, 0, -Standard_offset) - Vector3.forward)));
                    }
                    else //right
                    {
                        Arrow.Add((child.gameObject, new SingleUnit(Arrow[0].Item1.transform.position - new Vector3(Standard_offset, 0, Standard_offset) - Vector3.forward)));
                    }
                }
            }
        }
        GroupFormation arrowForm = new GroupFormation();
        arrowForm.idealformation = Arrow;
        arrowForm.name = "Arrow";
        PossibleFormations = new Dictionary<string, GroupFormation>();
        PossibleFormations.Add("Arrow", arrowForm);

        childMovements = new List<(GameObject, UnitMovement)>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.layer != 5)
                childMovements.Add((transform.GetChild(i).gameObject, transform.GetChild(i).GetComponent<UnitMovement>()));
        }
        SetFormation();
    }

    public void SetFormation() 
    { 
        var positions = GetImperfectPos("Arrow");
        for (int i = 0; i < childMovements.Count; i++)
        {
            var child = positions.Where(pos => pos.Item1.Equals(childMovements[i].Item1)).FirstOrDefault();
            Vector3 finalPos = child.Item2.FinalPos;
            finalPos = new Vector3(finalPos.x, child.Item1.transform.position.y, finalPos.z);
            childMovements[i].Item2.target = finalPos;
        }
    }

    private List<(GameObject, SingleUnit)> GetImperfectPos(string formationName)
    {
        var perfFormation = PossibleFormations["Arrow"];
        List<(GameObject, SingleUnit)> imperfectFormation = new List<(GameObject, SingleUnit)>();
        for (int i = 0; i < perfFormation.idealformation.Count; i++)
        {
            imperfectFormation.Add((perfFormation.idealformation[i].Item1, new SingleUnit(perfFormation.idealformation[i].Item2.FinalPos * Random.Range(0.95f, 1f))));
        }
        return imperfectFormation;
    }
}
