using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public List<SingleUnit> GroupUnits;
    public float morale;
    public float tiredness;
    public string CurrentFormation;

    private FormationHelper formationHelper;
    void Start()
    {
        formationHelper = transform.GetComponent<FormationHelper>();
        GroupUnits = new List<SingleUnit>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer != 5)
            {
                var curChild = transform.GetChild(i);
                GroupUnits.Add(new SingleUnit(curChild.gameObject, curChild.transform.position));
            }
        }
        float variation = 0.05f; //0.05 = 5%
        //now randomize some stats for variation
        for (int i = 0; i < GroupUnits.Count; i++)
        {
            var curUnit = GroupUnits[i];
            curUnit.UnitAgent.speed += Random.Range(-variation / 2.0f, variation / 2.0f);
            curUnit.UnitAgent.acceleration += Random.Range(-variation / 2.0f, variation / 2.0f);
        }
    }
    private void Update()
    {
        RecheckGroup();
    }
    private IEnumerable RecheckGroup()
    {
        for (; ; )
        {
            GroupUnits = new List<SingleUnit>();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.layer != 5)
                {
                    var curChild = transform.GetChild(i);
                    GroupUnits.Add(new SingleUnit(curChild.gameObject, curChild.transform.position));
                }
            }
            yield return new WaitForSeconds(.1f);
        }        
    }
    /// <summary>
    /// sets a target for the group, accounting for formation offset
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Vector3 newTarget)
    {
        //set first unit as proper target;
        GroupUnits[0].UnitAgent.destination = newTarget;
        for (int i = 0; i < GroupUnits.Count; i++)
        {
            GroupUnits[i].UnitAgent.destination = newTarget;
        }
        formationHelper.SetFormation();
    }
}
