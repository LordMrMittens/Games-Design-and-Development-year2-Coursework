using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : Mover
{
    HealthManager healthManager;
    public override void Start()
    {
        healthManager = GetComponent<HealthManager>();
        base.Start();
        rotation = 0;
        altitude = -5;
        GameManager.gameManager.playerIsAlive = true;
    }
    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        if (altitude <= -5)
        {
            altitude = -5;
        }
        else if (altitude >= 5)
        {
            altitude = 5;
        }
        base.Move(horizontalMovement, verticalMovement);
    }

    public void ResetOnRespawn()
    {
        healthManager.health = healthManager.maxHealth;
    }
}
