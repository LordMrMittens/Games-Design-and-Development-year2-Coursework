using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : AgentMover
{
    public override void Start()
    {
        ResetStats();
    }

    private void ResetStats()
    {
        base.Start();
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        FindPatrolNodes();
    }

    private void OnEnable()
    {
        ResetStats();
    }
    public virtual void Update()
    {
        Navigate();
    }

    public virtual void Navigate()
    {
        NavigateWaypoints();
        FindPlayer();
    }
}

