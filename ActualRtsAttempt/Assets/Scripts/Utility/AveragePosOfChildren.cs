using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveragePosOfChildren : MonoBehaviour
{
    public Vector3 AveragePosition = Vector3.zero;
    void Update()
    {
        Vector3 avgPos = Vector3.zero;
        for (int i = 0; i < transform.childCount; ++i)
            avgPos += transform.GetChild(i).position;

        avgPos /= transform.childCount;

        AveragePosition = avgPos;
    }
}
