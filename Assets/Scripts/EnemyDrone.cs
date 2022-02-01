using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyDrone : EnemyPatrol
{
    enum State { Patrolling, Chasing, Orbiting}
    State state;
    List<GameObject> flankingPoints = new List<GameObject>();
    [SerializeField] float chaseSpeed;
    [SerializeField] float orbitDistance;
    [SerializeField] float orbitSpeed;
    [SerializeField] float dodgeSpeed;
    GameObject orbitCentre;
    Vector3 orbitOrigin;
    NavMeshAgent navAgent;
    Transform targetFlankingPoint;
    int currentFlankingPoint;
    
    public override void Start()
    {
        state = State.Patrolling;
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
    }
    public override void Update()
    {
        if (state == State.Patrolling)
        {
            Navigate();
        }
        if (state == State.Chasing)
        {
            ChasePlayer(player);
        }
        if (state == State.Orbiting)
        {
            Orbit();
        }
        
    }
    public override void ChasePlayer(GameObject target)
    {
        state = State.Chasing;
        flankingPoints.AddRange(GameObject.FindGameObjectsWithTag("FlankingPoint"));
        
        if (Vector3.Distance(transform.position, player.transform.position) > orbitDistance)
        {
            base.ChasePlayer(target);
        } else
        {
            
            if (flankingPoints.Count > 0)
            {
                currentFlankingPoint = 0;
                targetFlankingPoint = flankingPoints[currentFlankingPoint].transform;
                state = State.Orbiting;
            } else
            {
               
            }
            
        }
    }

    void Orbit()
    {
        float distance = Vector3.Distance(transform.position,player.transform.position);
        
        if (distance > orbitDistance)
        {
            navAgent.speed = chaseSpeed;
            navAgent.acceleration = 20;
        }
        else
        {
            navAgent.speed = orbitSpeed;
            transform.LookAt(player.transform, Vector3.forward);
        }
        
        agent.SetDestination(targetFlankingPoint.transform.position);
        
        if (Vector3.Distance(transform.position, targetFlankingPoint.position) < .5)
        {
            if ((currentFlankingPoint + 1) < flankingPoints.Count)
            {
                currentFlankingPoint++;

                targetFlankingPoint = flankingPoints[currentFlankingPoint].transform;
            }
            else
            {
                currentFlankingPoint = 0;
                targetFlankingPoint = flankingPoints[currentFlankingPoint].transform;
            }
            agent.SetDestination(targetFlankingPoint.position);
            
        }
    }
}
