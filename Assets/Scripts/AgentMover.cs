using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : Mover
{
    public NavMeshAgent agent;
    public Transform nextDestination;
    public int currentNode;
    public int setViewDistance;
    public float viewDistance;
    public GameObject player;
    public int searchInterval = 100;
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        base.Start();
    }
    public virtual void FindPatrolNodes()
    {
        
        currentNode = Random.Range(0, PatrolNodeSpawnController.PNSC.patrolNodes.Count);
        nextDestination = PatrolNodeSpawnController.PNSC.patrolNodes[currentNode].transform;
        agent.SetDestination(nextDestination.position);
       
    }
    public virtual void Update()
    {
        if (agent.enabled == true && nextDestination == null)
        {
            FindPatrolNodes();
        }
        else if (PatrolNodeSpawnController.PNSC.patrolNodes.Count > 0 && nextDestination != null)
        {
            NavigateWaypoints();
            FindPlayer();
        }
        else
        {
            agent.enabled = true;
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
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
    public virtual void NavigateWaypoints()
    {
        if (Vector3.Distance(transform.position, nextDestination.position) < 3)
        {
            if ((currentNode + 1) < PatrolNodeSpawnController.PNSC.patrolNodes.Count)
            {
                currentNode++;
                nextDestination = PatrolNodeSpawnController.PNSC.patrolNodes[currentNode].transform;
            }
            else
            {
                currentNode = 0;
                nextDestination = PatrolNodeSpawnController.PNSC.patrolNodes[currentNode].transform;
            }
            agent.SetDestination(nextDestination.position);
        }
        if(nextDestination== null)
        {
            currentNode = (Random.Range(0, PatrolNodeSpawnController.PNSC.patrolNodes.Count));
            nextDestination = PatrolNodeSpawnController.PNSC.patrolNodes[currentNode].transform;
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
    /*public NavMeshAgent agent;
    public List<GameObject> patrolNodes = new List<GameObject>();
    public Transform nextDestination;
    public int currentNode;
    public int setViewDistance;
    public float viewDistance;
    public GameObject player;
    public int searchInterval = 100;
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        base.Start();
    }
    public virtual void FindPatrolNodes()
    {
        patrolNodes.AddRange(GameObject.FindGameObjectsWithTag("patrol_node"));
        currentNode = 0;
        nextDestination = patrolNodes[currentNode].transform;
        agent.SetDestination(nextDestination.position);
    }
    public virtual void Update()
    {
        if (agent.enabled == true && patrolNodes.Count == 0)
        {
            FindPatrolNodes();
        }
        else if (patrolNodes.Count > 0 && nextDestination != null)
        {
            NavigateWaypoints();
            FindPlayer();
        }
        else
        {
            agent.enabled = true;
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
    }*/
}
