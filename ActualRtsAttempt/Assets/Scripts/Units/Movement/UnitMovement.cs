﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour, IMovement
{
    [Header("Unit movement stats")]
    public float dAccelaration = 0.1f;
    public float Acceleration = 0f; //TODO: showonly
    public float MaxAcceleration = 15f;
    public float Drag = 0.05f;
    private Vector3 _target;
    public Vector3 target
    {
        get { return _target; }
        set
        {
            _target = value;
            startTime = Time.time;
        }
    }
    private GameObject ParentSquadObj; //the parent squad object, its position can be used to automatically get the center of the group
    private NavMeshAgent agent;
    //lerp related variables
    public float startTime;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Acceleration > 0)
            Acceleration -= Drag * Time.deltaTime;
        if (Acceleration > MaxAcceleration) Acceleration = MaxAcceleration;
        if (Acceleration < 0) Acceleration = 0;
        if (target != null)
        {
            LookAt(new Vector3(target.x, transform.position.y, target.z));
            Acceleration += dAccelaration * Time.deltaTime;
            if (agent != null) agent.destination = target;
            else MoveToward(target);
        }
    }
    public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos); //TODO: should use quaternion math to factor in drag and y cords.
    }

    public void MoveToward(Vector3 pos)
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * dAccelaration;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / Vector3.Distance(transform.position, pos);

        transform.position = Vector3.Lerp(transform.position, pos, fractionOfJourney); //TODO: should be lerp
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
