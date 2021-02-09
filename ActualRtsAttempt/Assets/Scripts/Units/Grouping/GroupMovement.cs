using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Group movement executes the actual movement on the first child, the rest get their destinations using the standard offsets
/// </summary>
public class GroupMovement : MonoBehaviour, IMovement
{
    private Group group;
    private string _curFormation;
    public string CurFormation
    {
        get
        {
            return _curFormation; //TODO: enum
        }
        set
        {
            _curFormation = value;
            if (group != null && !group.CurrentFormation.Equals(_curFormation))
                group.CurrentFormation = _curFormation;
        }
    }
    private Vector3 _currentTarget;
    private Vector3 CurrentTarget {
        get
        {
            return _currentTarget;
        }
        set
        {
            _currentTarget = value;
            group.SetTarget(_currentTarget);
        }
    }
    void Start()
    {
        if (string.IsNullOrEmpty(CurFormation)) CurFormation = "Column"; //TODO: const or enum
        group = transform.GetComponent<Group>();
        if(group.GroupUnits != null && group.GroupUnits.Count > 0)
            CurrentTarget = group.GroupUnits[0].UnitObject.transform.position;
    }
    void Update()
    {
        CheckRotation();
    }
    private void CheckRotation()
    {
        if(group != null && group.GroupForward != Vector3.zero)
            LookAt(group.GroupForward);
    }
    public void LookAt(Vector3 pos)
    {
        int totalorientedUnits = 0;
        for (int i = 0; i < group.GroupUnits.Count; i++)
        {
            var curUnit = group.GroupUnits[i];
            if (Vector3.Distance(curUnit.UnitObject.transform.position, curUnit.UnitAgent.destination) <= curUnit.UnitAgent.stoppingDistance && !curUnit.IsOrientedWithGroup)
            {
                var rotation = Quaternion.LookRotation(pos.normalized);
                curUnit.UnitObject.transform.rotation = Quaternion.RotateTowards(curUnit.UnitObject.transform.rotation, rotation, 4);
                totalorientedUnits++;
            }
        }
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
