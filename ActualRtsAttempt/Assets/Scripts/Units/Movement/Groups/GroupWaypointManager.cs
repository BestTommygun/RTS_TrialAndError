using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupWaypointManager : MonoBehaviour
{
    public float distance = 2f; //The distance between the unit and a waypoint before the unit sees the waypoint as 'completed'.
    private GameObject _waypoint;
    /// <summary>
    /// The waypoint currently used, read-only
    /// </summary>
    public GameObject waypoint
    {
        get { return _waypoint; }
        set
        {
            _waypoint = value;
            if (unitMovement != null) unitMovement.target = _waypoint.transform.position;
        }
    }
    private UnitMovement unitMovement;
    /// <summary>
    /// Set a new waypoint, can be accessed by anyone
    /// </summary>
    /// <param name="newWaypoint">The new waypoint to be used</param>
    public void SetWaypoint(GameObject newWaypoint)
    {
        waypoint = newWaypoint;
    }
    void Start()
    {

    }
    private void Update()
    {
        if (_waypoint != null) //check distance to waypoint, delete if too waypoint is completed.
        {
            //factor out height
            Vector3 unitPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 waypointPos = new Vector3(_waypoint.transform.position.x, 0, _waypoint.transform.position.z);
            if (Vector3.Distance(unitPos, waypointPos) <= distance)
            {
                UnitWaypoint prevWayPoint = _waypoint.GetComponent<UnitWaypoint>();
                var nextpoint = prevWayPoint.GetNext();
                if (nextpoint != null)
                    waypoint = (nextpoint as UnitWaypoint).gameObject;
                prevWayPoint.UnitsWithWaypoint.Remove(gameObject);
            }
        }
    }
}
