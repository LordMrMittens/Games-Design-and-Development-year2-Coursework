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
    void Update()
    {
        if (GameManager.gameManager.playerCanMove)
        {
            float verticalInput = Input.GetAxis("Vertical");
            if (GameManager.gameManager.levelPhase == GameManager.Phase.PhaseOne)
            {
                verticalInput = 0;
            }
            Move(Input.GetAxis("Horizontal"), verticalInput + verticalMovement);
        }
        else { Move(0, verticalMovement); }
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        if (altitude <= -5)
        {
            altitude = -5;
        }
        base.Move(horizontalMovement, verticalMovement);
    }

    public void ResetOnRespawn()
    {
        healthManager.health = healthManager.maxHealth;
    }
}
