using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum State { Patrolling, Chasing, Orbiting }
public class EnemyDrone : EnemyPatrol
{
    
    public State state;
    public List<GameObject> flankingPoints = new List<GameObject>();
    [SerializeField] float chaseSpeed;
    [SerializeField] float orbitDistance;
    [SerializeField] float orbitSpeed;
    [SerializeField] float dodgeSpeed;
    GameObject orbitCentre;
    Vector3 orbitOrigin;
    NavMeshAgent navAgent;
    Transform targetFlankingPoint;
    int currentFlankingPoint;
    Gun gun;
    public override void Start()
    {
        base.Start();
        state = State.Patrolling;
        navAgent = GetComponent<NavMeshAgent>();
        gun = GetComponent<Gun>();
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        flankingPoints.AddRange(GameObject.FindGameObjectsWithTag("FlankingPoint"));
    }
    public override void Update()
    {
        if (state == State.Patrolling)
        {
            base.Update();
        }
        if (state == State.Chasing)
        {
            if (GameManager.TGM.playerIsAlive)
            {
                ChasePlayer(player);
            }
        }
        if (state == State.Orbiting)
        {
            Orbit();
        }
        if (!GameManager.TGM.playerIsAlive)
        {
            state = State.Patrolling;
        }
    }
    public override void ChasePlayer(GameObject target)
    {
        state = State.Chasing;
        

        if (Vector3.Distance(transform.position, player.transform.position) > orbitDistance && target != null)
        {
            base.ChasePlayer(target);
        }
        else
        {

            if (flankingPoints.Count > 0)
            {
                currentFlankingPoint = Random.Range(0,flankingPoints.Count);
                targetFlankingPoint = flankingPoints[currentFlankingPoint].transform;
                state = State.Orbiting;
            }
        }
    }
    void Orbit()
    {
        if (GameManager.TGM.playerIsAlive)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance > orbitDistance)
                {
                    navAgent.speed = chaseSpeed;
                    navAgent.acceleration = 20;
                }
                else
                {
                    navAgent.speed = orbitSpeed;
                    
                    gun.Shoot();
                }
                agent.SetDestination(targetFlankingPoint.transform.position);
                if (Vector3.Distance(transform.position, targetFlankingPoint.position) < .5)
                {
                    if (Random.Range(0, 2) != 0)
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
                    }
                    else
                    {
                        currentFlankingPoint--;
                        if (currentFlankingPoint < 0)
                        {
                            currentFlankingPoint = flankingPoints.Count-1;
                        }
                        targetFlankingPoint = flankingPoints[currentFlankingPoint].transform;
                    }
                    agent.SetDestination(targetFlankingPoint.position);
                }
            }
        } else
        {
            state = State.Patrolling;
        }
    }
}
