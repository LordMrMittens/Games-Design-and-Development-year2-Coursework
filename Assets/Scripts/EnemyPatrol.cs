using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : AgentMover
{
    public override void Start()
    {
        base.Start();
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
    }
    public override void Update()
    {
        base.Update();
    }
}

