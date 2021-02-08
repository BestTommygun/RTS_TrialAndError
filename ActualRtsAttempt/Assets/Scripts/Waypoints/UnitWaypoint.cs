using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWaypoint : StandardWaypoint
{
    public List<GameObject> UnitsWithWaypoint;
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
        if (UnitsWithWaypoint == null || UnitsWithWaypoint.Count <= 0)
        {
            if (nextWaypoint != null)
            {
                for (int i = 0; i < UnitsWithWaypoint.Count; i++)
                {
                    var waypointManager = UnitsWithWaypoint[i].GetComponent<UnitWaypointManager>();
                    if (nextWaypoint.GetType() == typeof(UnitWaypoint))
                        waypointManager.SetWaypoint(((UnitWaypoint)nextWaypoint).gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
