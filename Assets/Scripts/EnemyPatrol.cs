using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : AgentMover
{
    public override void Start()
    {
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
    private void OnTriggerEnter(Collider other)
    {
        
    }
}

