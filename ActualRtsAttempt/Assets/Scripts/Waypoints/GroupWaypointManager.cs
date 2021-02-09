using Assets.Scripts.Waypoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupWaypointManager : MonoBehaviour, IWaypointManager
{
    public float distance = 2f; //The distance between the unit and a waypoint before the unit sees the waypoint as 'completed'.
    private GameObject _waypoint;
    private GroupMovement GroupMovement;
    /// <summary>
    /// The waypoint currently used, read-only
    /// </summary>
    public GameObject waypoint
    {
        get { return _waypoint; }
        set
        {
            _waypoint = value;
            GroupMovement.SetTarget(_waypoint.transform.position);
        }
    }
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
        GroupMovement = transform.GetComponent<GroupMovement>();
    }
}
