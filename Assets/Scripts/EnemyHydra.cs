using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHydra : AgentMover
{
    enum status { Passive, Active, Attached}
    status state;
    bool isDivided = false;
    public float timeBetweenMovements;
    float movementCounter;
    
    public override void Start()
    {
        base.Start();
        movementCounter = timeBetweenMovements;
        agent.enabled=false;
        
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case status.Passive:
                MoveErratically();
                FindPlayer();
                break;
            case status.Active:
                
                ChasePlayer(player);
                break;
            case status.Attached:
                Debug.Log("Code this behaviour");
                break;
        }
            
    }
    private void MoveErratically()
    {
        movementCounter -= Time.deltaTime;
        if (movementCounter < 0)
        {
            horizontalMovement = Random.Range(-1, 2);
            verticalMovement = Random.Range(-1, 2);
            movementCounter = timeBetweenMovements;
        }

        Move(horizontalMovement, verticalMovement);
    }
    public override void FindPlayer()
    {
        if (player != null)
        {
            
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < viewDistance)
            {
                agent.enabled = true;
                viewDistance = 100;
                state = status.Active;

            }
        }
        else
        {
            if (Time.frameCount % searchInterval == 0)
            {
                player = GameObject.Find("Player");
            
            }
        }
    }
    public override void ChasePlayer(GameObject target)
    {
        base.ChasePlayer(target);
        KeepFacingCenter();
    }
}
