using Assets.Scripts.Units.Grouping;
using Assets.Scripts.Units.Grouping.Formations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationHelper : MonoBehaviour
{
    public Group group;
    public FormationFactory formationFactory;
    public float Standard_offset = 2f; //TODO: const
    void Start()
    {
        if (group == null) group = transform.GetComponent<Group>();
        formationFactory = new FormationFactory();
        //TODO: use factory
        if(group.GroupUnits != null)
            SetFormation();
    }

    public void SetFormation()
    { 
        var positions = GetImperfectPos("Column", 0.05f);
        for (int i = 0; i < group.GroupUnits.Count; i++)
        {
            var curUnit = group.GroupUnits[i];
            var formationUnit = positions.Where(pos => pos.Item1.GetInstanceID().Equals(curUnit.UnitObject.GetInstanceID())).FirstOrDefault();
            curUnit.UnitAgent.destination += formationUnit.Item2;
        }
    }

    private List<(GameObject, Vector3)> GetImperfectPos(string formationName, float errorMargin = 0.1f)
    {
        IFormation formation = formationFactory.returnFormation(formationName);
        var perfFormation = formation.ApplyFormationLogic(transform);
        List<(GameObject, Vector3)> imperfectFormation = new List<(GameObject, Vector3)>();
        for (int i = 0; i < perfFormation.Count; i++)
        {
            imperfectFormation.Add((perfFormation[i].Item1, perfFormation[i].Item2 * Random.Range(1f - (errorMargin / 2), 1f + (errorMargin / 2f))));
        }
        return imperfectFormation;
    }
}
