using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupWaypoint : StandardWaypoint
{
    public List<Group> GroupsWithWaypoint = new List<Group>();
    private Vector3 _waypointForward;
    public Vector3 WayPointForward
    {
        get
        {
            return _waypointForward;
        }
        set
        {
            _waypointForward = value;
            Left = new Vector3(_waypointForward.z, _waypointForward.y, -_waypointForward.x) * -1;
        }
    }
    public Vector3 Left;
    void Start()
    {
        CheckIfShouldDestroy();
    }

    void Update()
    {
        CheckIfShouldDestroy();
    }

    private void CheckIfShouldDestroy() //maybe make IEnumerable for coroutines
    {
        if (GroupsWithWaypoint == null || GroupsWithWaypoint.Count <= 0)
        {
            if (nextWaypoint != null)
            {
                for (int i = 0; i < GroupsWithWaypoint.Count; i++)
                {
                    var waypointManager = GroupsWithWaypoint[i].GetComponent<GroupWaypointManager>();
                    if (nextWaypoint.GetType() == typeof(GroupWaypoint))
                        waypointManager.SetWaypoint(((GroupWaypoint)nextWaypoint).gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
