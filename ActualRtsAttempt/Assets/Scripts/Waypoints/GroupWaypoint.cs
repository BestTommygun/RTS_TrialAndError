using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupWaypoint : StandardWaypoint
{
    public List<Group> GroupsWithWaypoint = new List<Group>();
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
