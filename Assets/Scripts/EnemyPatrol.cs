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
        patrolNodes.AddRange(GameObject.FindGameObjectsWithTag("patrol_node"));
        if (patrolNodes.Count > 0)
        {
            currentNode = 0;
            nextDestination = patrolNodes[currentNode].transform;
            agent.SetDestination(nextDestination.position);
        }
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

