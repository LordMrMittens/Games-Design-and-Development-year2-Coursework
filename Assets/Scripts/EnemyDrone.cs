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
            //Debug.Log("Patrolling");
        }
        if (state == State.Chasing)
        {
            ChasePlayer(player);
            //Debug.Log("Chasing");
        }
        if (state == State.Orbiting)
        {
            //Debug.Log("Orbiting");
            Orbit();
           // KeepDistance();
            //orbit player
            
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
               // state = State.Patrolling;
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
            /*
            distance = orbitDistance;
            transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;*/
        }
        else
        {
            navAgent.speed = orbitSpeed;
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

    /*
     void Orbit(float horizontalMovement)
    {
        float distance = Vector3.Distance(transform.position, orbitCentre.transform.position);
        if (distance != orbitDistance)
        {
            distance = orbitDistance;
            transform.position = (transform.position - orbitCentre.transform.position).normalized * distance + orbitCentre.transform.position;
            
        }
        //rotation -= horizontalMovement * speed * Time.deltaTime;
        transform.RotateAround(orbitCentre.transform.localPosition, orbitCentre.transform.forward, orbitSpeed * Time.deltaTime);
        
        //transform.position = orbitOrigin + Quaternion.Euler(rotation,0,0) * new Vector3(0, orbitDistance, 0);
        //transform.LookAt(new Vector3(orbitOrigin.x, orbitOrigin.y, orbitOrigin.z), Vector3.left);
    }
    void KeepDistance()
    {
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, elevation, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
    }*/
}
