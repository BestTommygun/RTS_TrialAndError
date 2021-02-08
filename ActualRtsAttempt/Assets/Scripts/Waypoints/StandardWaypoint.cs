using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandardWaypoint : MonoBehaviour, IWaypoint
{
    protected IWaypoint nextWaypoint;
    protected IWaypoint prevWaypoint;

    public IWaypoint GetNext()
    {
        return nextWaypoint;
    }

    public void SetNext(IWaypoint waypoint)
    {
        nextWaypoint = waypoint;
    }
    public IWaypoint GetPrev()
    {
        return prevWaypoint;
    }

    public void SetPrev(IWaypoint waypoint)
    {
        prevWaypoint = waypoint;
    }

    /// <summary>
    /// Recursivly calls entire chian to destroy itself.
    /// </summary>
    public void Destroy()
    {
        if(prevWaypoint != null)
            prevWaypoint.Destroy();
        Destroy(gameObject);
    }
}
