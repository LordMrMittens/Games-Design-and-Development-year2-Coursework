using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : AgentMover
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
    void Update()
    {
        Navigate();
    }

    private void Navigate()
    {
        NavigateWaypoints();
        FindPlayer();
    }

    }

