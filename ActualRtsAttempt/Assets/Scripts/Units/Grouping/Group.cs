using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public List<SingleUnit> GroupUnits;
    public float morale;
    public float tiredness;
    private GroupMovement groupMovement;
    public string defaultFormation = "Column";
    public Vector3 currentTarget;
    private string _currentFormation;
    public string CurrentFormation 
    {
        get 
        {
            return _currentFormation;
        }
        set
        {
            if(!groupMovement.CurFormation.Equals(_currentFormation))
                groupMovement.CurFormation = _currentFormation;
            _currentFormation = value;
            formationHelper.SetFormation(_currentFormation);

        }
    }
    public Vector3 GroupForward = Vector3.forward;
    public Vector3 GroupLeft = Vector3.forward;
    public Vector3 GroupRight = Vector3.forward;
    public Vector3 GroupBackward = Vector3.forward;

    private FormationHelper formationHelper;
    void Start()
    {
        groupMovement = transform.GetComponent<GroupMovement>();
        formationHelper = transform.GetComponent<FormationHelper>();
        GroupUnits = new List<SingleUnit>();

        if (string.IsNullOrEmpty(_currentFormation)) _currentFormation = defaultFormation;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer != 5)
            {
                var curChild = transform.GetChild(i);
                GroupUnits.Add(new SingleUnit(curChild.gameObject, curChild.transform.position));
            }
        }
        float variation = 0.9f; //0.05 = 5%
        //now randomize some stats for variation
        for (int i = 0; i < GroupUnits.Count; i++)
        {
            var curUnit = GroupUnits[i];
            curUnit.UnitAgent.acceleration = curUnit.UnitAgent.acceleration * Random.Range(1 - variation / 2.0f, 1 + variation / 2.0f);
        }
        SetTarget(GroupUnits[0].UnitObject.transform.position);
    }
    private void Update()
    {
        RecheckGroup();
        Debug.DrawLine(transform.position, transform.position + GroupForward, Color.yellow);
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
        for (int i = 0; i < GroupUnits.Count; i++)
        {
            GroupUnits[i].UnitAgent.destination = newTarget;
        }
        formationHelper.SetFormation();
        currentTarget = newTarget;

        // set normalized axis, usefull for formation classes
        GroupForward = (newTarget - transform.position).normalized;
        GroupBackward = -GroupForward;
        GroupRight = new Vector3(GroupForward.z, GroupForward.y, -GroupForward.x);
        GroupLeft = -GroupRight;
    }
    public void SetTarget(Vector3 newTarget, Vector3 NewForward)
    {
        //set first unit as proper target;
        GroupUnits[0].UnitAgent.destination = newTarget;
        for (int i = 0; i < GroupUnits.Count; i++)
        {
            GroupUnits[i].UnitAgent.destination = newTarget;
        }
        formationHelper.SetFormation();

        // set normalized axis, usefull for formation classes
        GroupForward = NewForward.normalized;
        GroupBackward = -GroupForward;
        GroupRight = new Vector3(GroupForward.z, GroupForward.y, -GroupForward.x);
        GroupLeft = -GroupRight;
    }
}
