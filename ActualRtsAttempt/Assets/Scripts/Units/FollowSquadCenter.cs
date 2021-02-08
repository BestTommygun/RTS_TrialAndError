using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSquadCenter : MonoBehaviour
{
    public AveragePosOfChildren CenterScript;
    public Vector3 offset;
    void Update()
    {
        if (CenterScript != null)
            transform.position = CenterScript.AveragePosition + offset;
    }
}
