using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public List<SingleUnit> GroupUnits;
    public float morale;
    public float tiredness;
    public string CurrentFormation;
    void Start()
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
}
