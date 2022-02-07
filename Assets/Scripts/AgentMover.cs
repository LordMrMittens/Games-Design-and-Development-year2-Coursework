using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : Mover
{
    public NavMeshAgent agent;
    public List<GameObject> patrolNodes = new List<GameObject>();
    public Transform nextDestination;
    public int currentNode;
    public int setViewDistance;
    public float viewDistance;
    public GameObject player;
    public int searchInterval = 100;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void FindPatrolNodes()
    {
        patrolNodes.AddRange(GameObject.FindGameObjectsWithTag("patrol_node"));
        
        if (patrolNodes.Count > 0 && agent.enabled == true)
        {
            currentNode = 0;
            nextDestination = patrolNodes[currentNode].transform;
            agent.SetDestination(nextDestination.position);
        }
    }
    private void Update()
    {
        if(agent.enabled ==true && patrolNodes.Count == 0)
        {
            FindPatrolNodes();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }
    public virtual void FindPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < viewDistance)
            {

                ChasePlayer(player);

            }
            else
            {
                agent.SetDestination(nextDestination.position);
            }
        }
        else
        {
            if (Time.frameCount % searchInterval == 0)
            {
               // Debug.Log(name + " Searched for player");
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    public virtual void NavigateWaypoints()
    {
        if (Vector3.Distance(transform.position, nextDestination.position) < 3)
        {
            if ((currentNode + 1) < patrolNodes.Count)
            {
                //currentNode++;
                currentNode = (Random.Range(0, patrolNodes.Count));
                nextDestination = patrolNodes[currentNode].transform;
            }
            else
            {
                currentNode = 0;
                nextDestination = patrolNodes[currentNode].transform;
            }
            agent.SetDestination(nextDestination.position);
        }
    }
    private void OnDisable()
    {
        agent.enabled = false;
    }
    public virtual void ChasePlayer(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }
}
