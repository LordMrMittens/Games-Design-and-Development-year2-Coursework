using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : Mover
{

    public override void Start()
    {
        base.Start();
        rotation = 0;
        altitude = -5;
    }
    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    public override void Move(float horizontalMovement,float verticalMovement)
    {
        if (altitude <=-5) 
        { 
            altitude = -5;
        } else if (altitude >= 5)
        {
            altitude = 5;
        }  
        base.Move(horizontalMovement,verticalMovement);
        

    }
}
