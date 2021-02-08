using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    IWaypoint GetNext();
    void SetNext(IWaypoint waypoint);
    IWaypoint GetPrev();
    void SetPrev(IWaypoint waypoint);
    void Destroy();
}
