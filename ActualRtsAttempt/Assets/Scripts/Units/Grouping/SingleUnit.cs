using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct SingleUnit //saves certain unit components and stats for easy acces without constantly having to call getcomponent.
{ //TODO: units should have veeeeery slightly varying speed for added realism
    public SingleUnit(GameObject unit, Vector3 pos)
    {
        FinalPos        = pos;
        UnitObject      = unit;
        MovementScript  = UnitObject.GetComponent<UnitMovement>();
        UnitAgent       = UnitObject.GetComponent<NavMeshAgent>();
        WaypointManager = UnitObject.GetComponent<UnitWaypointManager>();
        MainCollider    = UnitObject.GetComponent<CapsuleCollider>();
    }
    public Vector3 FinalPos;
    public readonly GameObject UnitObject;
    public UnitMovement MovementScript;
    public NavMeshAgent UnitAgent;
    public UnitWaypointManager WaypointManager;
    public CapsuleCollider MainCollider;
}
