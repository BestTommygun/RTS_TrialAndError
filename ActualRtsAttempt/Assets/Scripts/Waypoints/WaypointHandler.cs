using SelectionSystem;
using SelectionSystem.Components;
using SelectionSystem.DesignedCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    [Tooltip("The waypoint gameobject used by any selected units, if none an empty gameobject will be used instead.")]
    public GameObject WaypointPrefab;
    private Tuple<SelectionList<ISelectable>, GameObject> prevWayPointGenerated;

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
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = GetWaypointPos();
            if (!pos.Equals(Vector3.zero)) GenerateWayPoint(pos);
        }
    }

    public void GenerateWayPoint(Vector3 pos)
    {
        if (pos.Equals(Vector3.zero) || selectionHandler.currentSelection == null || selectionHandler.currentSelection.Count <= 0) return;

        GameObject newWaypoint;
        if (WaypointPrefab != null)
        {
            newWaypoint = GameObject.Instantiate<GameObject>(WaypointPrefab);
            if(newWaypoint.GetComponent<StandardWaypoint>() == null)
            {
                Debug.LogError("The waypoint prefab MUST be provided a script component of StandardWaypoint type or subtype");
                return;
            }
        }
        else
        {
            newWaypoint = GameObject.Instantiate<GameObject>(new GameObject("Basic Waypoint"));
            newWaypoint.AddComponent<UnitWaypoint>();
        }
        newWaypoint.transform.position = pos;
        StandardWaypoint newWaypointScript = newWaypoint.GetComponent<UnitWaypoint>();
        //check if we should chain waypoints
        if (prevWayPointGenerated != null)
        {
            if(prevWayPointGenerated.Item1.Equals(selectionHandler.currentSelection))
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
                else if(prevWayPointGenerated.Item2 != null)
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
            if(selectedUnit.GetType().Equals(typeof(Selector)))
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
    private Vector3 GetWaypointPos()
    {
        Vector3 pos = Vector3.zero;
        Ray ray = camera.ScreenPointToRay(currentCursorPosition);

        if (Physics.Raycast(ray, out var hit, camera.farClipPlane, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            pos = hit.point;
        }
        return pos;
    }
}
