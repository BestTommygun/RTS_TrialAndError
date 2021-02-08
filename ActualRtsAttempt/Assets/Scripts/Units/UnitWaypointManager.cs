using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWaypointManager : MonoBehaviour
{
    public float distance = 0.5f; //The distance between the unit and a waypoint before the unit sees the waypoint as 'completed'.
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
            if (unitMovement != null) unitMovement.target = _waypoint;
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
        unitMovement = gameObject.GetComponent<UnitMovement>();
    }
    private void Update()
    {
        if(_waypoint != null) //check distance to waypoint, delete if too waypoint is completed.
        {
            if (Vector3.Distance(transform.position, _waypoint.transform.position) <= distance)
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
