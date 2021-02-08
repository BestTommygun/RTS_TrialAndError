using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void SetTarget(GameObject target);
    void MoveToward(Vector3 pos);
    void LookAt(Vector3 pos);
}
