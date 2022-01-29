using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : AgentMover
{


    // Start is called before the first frame update
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

    // Update is called once per frame
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

