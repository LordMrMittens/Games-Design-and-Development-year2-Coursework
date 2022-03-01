using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : Mover
{
    [SerializeField] float timeBetweenMovements;
    float movementCounter;
    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        MoveErratically();
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

}
