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
        rotation = transform.eulerAngles.y;
        origin = center.transform.position;
        movementCounter = timeBetweenMovements;
        
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
        Debug.Log(transform.position);
        base.ChasePlayer(target);
        Debug.Log(transform.position);
    }
    /*
    void ChaseThePlayer()
    {
        status = State.Active;
        float myrotation = rotation;
        float playerRotation = playerController.rotation;
        
        if (elevation < playerController.elevation - .2)
        {
            verticalMovement = movementSpeed;

        } else if (elevation > playerController.elevation+.2)
        {
            verticalMovement = movementSpeed * -1;

        } else
        {
            verticalMovement = 0;

        }
        if (Mathf.Abs(rotation % 360) > Mathf.Abs(playerRotation%360))
       
        {
            horizontalMovement = movementSpeed;
            //Debug.Log("first Statement");
        }
        else //if (Mathf.Abs(rotation%360) < Mathf.Abs(playerRotation%360))
        {
            horizontalMovement = -movementSpeed;
            //Debug.Log("Second Statement");
        }
        Debug.Log(Mathf.Abs(rotation % 360) + " substraction of both math abs rotations");
        
        //Move(horizontalMovement, verticalMovement);
        
    }*/
}
