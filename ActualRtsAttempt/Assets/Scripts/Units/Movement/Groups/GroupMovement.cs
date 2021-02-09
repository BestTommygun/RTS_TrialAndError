using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMovement : MonoBehaviour, IMovement
{
    private List<SingleUnit> units;
    public string CurFormation;
    void Start()
    {
        CurFormation = "Arrow";
        units = new List<SingleUnit>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.layer != 5)
            {
                var curChild = transform.GetChild(i);
                units.Add(new SingleUnit(curChild.gameObject, curChild.transform.position));
            }
        }
    }

    void Update()
    {

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
        throw new System.NotImplementedException();
    }
}
