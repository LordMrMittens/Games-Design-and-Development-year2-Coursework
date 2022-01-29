using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingEnemy : Mover
{
    [SerializeField] int timeToChangeDirection;
    [SerializeField] int verticalDistanceToTravel;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rotation = 270;
        
        horizontalMovement = movementSpeed;
        setHorizontalMovement = horizontalMovement;
        StartCoroutine(changeDirection());
    }
    // Update is called once per frame
    void Update()
    {
        Move(horizontalMovement, verticalMovement);
    }
    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(timeToChangeDirection);
        horizontalMovement = 0;
        setHorizontalMovement *= -1;
        verticalMovement = -verticalDistanceToTravel;
        yield return new WaitForSeconds(.3f);
        verticalMovement = 0;
        horizontalMovement = setHorizontalMovement;
        if(transform.position.y > 0)
        {
            StartCoroutine(changeDirection());
        }
    }
}
