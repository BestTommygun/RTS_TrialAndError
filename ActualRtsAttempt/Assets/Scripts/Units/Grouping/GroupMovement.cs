using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Group movement executes the actual movement on the first child, the rest get their destinations using the standard offsets
/// </summary>
public class GroupMovement : MonoBehaviour, IMovement
{
    private Group group;
    public string CurFormation; //TODO: enum
    private string defaultFormation;
    private Vector3 _currentTarget;
    private Vector3 CurrentTarget {
        get
        {
            return _currentTarget;
        }
        set
        {
            _currentTarget = value;

            group.SetTarget(value);
        }
    }
    void Start()
    {
        CurFormation = "Arrow";
        group = transform.GetComponent<Group>();
        if(group.GroupUnits != null && group.GroupUnits.Count > 0)
            CurrentTarget = group.GroupUnits[0].UnitObject.transform.position;
    }
    public void LookAt(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    public void MoveToward(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(Vector3 target)
    {
        CurrentTarget = target;
    }
}
