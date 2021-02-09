using SelectionSystem;
using SelectionSystem.Components;
using SelectionSystem.DesignedCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    [Tooltip("The waypoint gameobject used by any selected units, if none an empty gameobject will be used instead.")]
    public GameObject WaypointPrefab;
    private Tuple<SelectionList<ISelectable>, GameObject> prevWayPointGenerated;

    private Vector3 mousedownPoint;

    private SelectionHandler selectionHandler;
    private bool shiftPressed = false;
    private Vector3 currentCursorPosition = Vector3.zero;
    [SerializeField]
    [Tooltip("The camera that will be used to cast the selection box. If none is assigned, the Camera.main will be used instead.")]
    private Camera _camera;
    /// <summary>
    /// The camera used in selection operations.
    /// </summary>
    public Camera camera
    {
        get
        {
            if (!_camera)
                _camera = Camera.main;

            return _camera;
        }
        set
        {
            _camera = value;
        }
    }
    void Start()
    {
        selectionHandler = gameObject.GetComponent<SelectionHandler>();
        if (selectionHandler == null) Debug.LogError("Gameobject " + gameObject.name + " should also contain the Selection Handler script");
    }

    void Update()
    {
        currentCursorPosition = Input.mousePosition; //TODO: Use inputHelper for this
        shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Input.GetMouseButtonDown(1)) mousedownPoint = GetWaypointPos();
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 pos = GetWaypointPos();
            Vector3 forward = Vector3.zero;
            if (Vector3.Distance(pos, mousedownPoint) > 0)
            {
                forward = (pos - mousedownPoint).normalized;
                pos = mousedownPoint;
            }
            string typeofSelection = "Unit";
            if (selectionHandler.currentSelection.OfType<GroupSelector>().Count() > 0)
                typeofSelection = "Group";
            if (!pos.Equals(Vector3.zero)) GenerateWayPoint(pos, typeofSelection, forward);
        }
    }

    public void GenerateWayPoint(Vector3 pos, string typeofWaypoint, Vector3 forward) //TODO enum
    {
        if (pos.Equals(Vector3.zero) || selectionHandler.currentSelection == null || selectionHandler.currentSelection.Count <= 0) return;

        switch (typeofWaypoint) //TODO: use templating for just one function, no reason to duplicate code
        {
            case "Unit":
                GenerateUnitWaypoint(pos, forward);
                break;
            case "Group":
                GenerateGroupWaypoint(pos, forward);
                break;
            default:
                break;
        }

    }
    private void GenerateUnitWaypoint(Vector3 pos, Vector3 forward)
    {
        GameObject newWaypoint;
        if (WaypointPrefab != null)
        {
            newWaypoint = GameObject.Instantiate<GameObject>(WaypointPrefab);
            newWaypoint.AddComponent<UnitWaypoint>();
        }
        else
        {
            newWaypoint = GameObject.Instantiate<GameObject>(new GameObject("Unit Waypoint"));
            newWaypoint.AddComponent<UnitWaypoint>();
        }
        newWaypoint.transform.position = pos;
        if(forward != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(forward.normalized);
            newWaypoint.transform.rotation = Quaternion.RotateTowards(newWaypoint.transform.rotation, rotation, 720);
        }

        StandardWaypoint newWaypointScript = newWaypoint.GetComponent<UnitWaypoint>();

        //check if we should chain waypoints
        if (prevWayPointGenerated != null)
        {
            if (prevWayPointGenerated.Item1.Equals(selectionHandler.currentSelection))
            {
                if (shiftPressed)
                {
                    if (prevWayPointGenerated.Item2 != null)
                    {
                        var waypointscript = prevWayPointGenerated.Item2.GetComponent<StandardWaypoint>();
                        waypointscript.SetNext(newWaypointScript);
                        newWaypointScript.SetPrev(waypointscript);
                    }
                    else prevWayPointGenerated = null;
                }
                else if (prevWayPointGenerated.Item2 != null)
                {
                    var waypointScript = prevWayPointGenerated.Item2.GetComponent<StandardWaypoint>();
                    if (waypointScript) waypointScript.Destroy();
                    else Destroy(waypointScript.gameObject);
                }
                else
                {
                    prevWayPointGenerated = null;
                }
            }
        }
        UnitWaypoint unitWaypoint = newWaypoint.GetComponent<UnitWaypoint>();
        foreach (var selectedUnit in selectionHandler.currentSelection)
        {
            if (selectedUnit.GetType().Equals(typeof(Selector)))
            {
                Selector selector = selectedUnit as Selector;

                if (!shiftPressed || prevWayPointGenerated == null)
                {
                    UnitWaypointManager waypointManager;
                    waypointManager = selector.gameObject.GetComponent<UnitWaypointManager>();
                    if (waypointManager == null)
                        waypointManager = selector.gameObject.AddComponent<UnitWaypointManager>();
                    waypointManager.SetWaypoint(newWaypoint);
                }

                unitWaypoint.UnitsWithWaypoint.Add(selector.gameObject);
            }
        }
        prevWayPointGenerated = new Tuple<SelectionList<ISelectable>, GameObject>(selectionHandler.currentSelection, newWaypoint); //TODO: check that this isnt passing by reference
    }
    private void GenerateGroupWaypoint(Vector3 pos, Vector3 forward)
    {
        GameObject newWaypoint;

        if (WaypointPrefab != null)
        {
            newWaypoint = GameObject.Instantiate<GameObject>(WaypointPrefab);
            newWaypoint.AddComponent<GroupWaypoint>();
        }
        else
        {
            newWaypoint = GameObject.Instantiate<GameObject>(new GameObject("Group Waypoint"));
            newWaypoint.AddComponent<GroupWaypoint>();
        }
        newWaypoint.transform.position = pos;
        if (forward != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(forward.normalized);
            newWaypoint.transform.rotation = Quaternion.RotateTowards(newWaypoint.transform.rotation, rotation, 720);
        }
        GroupWaypoint newWaypointScript = newWaypoint.GetComponent<GroupWaypoint>();

        var selectedGroups = selectionHandler.currentSelection.OfType<GroupSelector>().ToList();

        //check if we should chain waypoints
        if (prevWayPointGenerated != null)
        {
            if (prevWayPointGenerated.Item1.Equals(selectionHandler.currentSelection))
            {
                if (shiftPressed)
                {
                    if (prevWayPointGenerated.Item2 != null)
                    {
                        var waypointscript = prevWayPointGenerated.Item2.GetComponent<StandardWaypoint>();
                        waypointscript.SetNext(newWaypointScript);
                        newWaypointScript.SetPrev(waypointscript);
                    }
                    else prevWayPointGenerated = null;
                }
                else if (prevWayPointGenerated.Item2 != null)
                {
                    var waypointScript = prevWayPointGenerated.Item2.GetComponent<StandardWaypoint>();
                    if (waypointScript) waypointScript.Destroy();
                    else Destroy(waypointScript.gameObject);
                }
                else
                {
                    prevWayPointGenerated = null;
                }
            }
        }
        for (int i = 0; i < selectedGroups.Count; i++)
        {
            GroupSelector selector = selectedGroups[i];

            if (!shiftPressed || prevWayPointGenerated == null)
            {
                GroupWaypointManager waypointManager;
                waypointManager = selector.gameObject.GetComponent<GroupWaypointManager>();
                if (waypointManager == null)
                    waypointManager = selector.gameObject.AddComponent<GroupWaypointManager>();
                waypointManager.SetWaypoint(newWaypoint);
            }

            newWaypointScript.GroupsWithWaypoint.Add(selector.gameObject.GetComponent<Group>());
        }

        prevWayPointGenerated = new Tuple<SelectionList<ISelectable>, GameObject>(selectionHandler.currentSelection, newWaypoint); //TODO: check that this isnt passing by reference
    }
    private Vector3 GetWaypointPos()
    {
        Vector3 pos = Vector3.zero;
        Ray ray = camera.ScreenPointToRay(currentCursorPosition);
        int layerMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out var hit, camera.farClipPlane, layerMask, QueryTriggerInteraction.Ignore))
        {
            pos = hit.point;
        }
        return pos;
    }
}
