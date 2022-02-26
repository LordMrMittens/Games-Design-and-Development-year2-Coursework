using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : Mover
{
    HealthManager healthManager;
    CameraFollow cameraControls;
    public override void Start()
    {
        healthManager = GetComponent<HealthManager>();
        base.Start();
        GameManager.TGM.playerIsAlive = true;
        cameraControls = Camera.main.GetComponent<CameraFollow>();
    }
    void Update()
    {
        if (GameManager.TGM.playerCanMove)
        {
            float verticalInput = Input.GetAxis("Vertical");
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
            {
                verticalInput = 0;
            }
            Move(Input.GetAxis("Horizontal"), verticalInput + verticalMovement);
        }
        else { Move(0,verticalMovement); }
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        if (altitude < cameraControls.altitude - cameraControls.playerLowerBounds)
        {
                altitude = cameraControls.altitude - cameraControls.playerLowerBounds;
        }
            if(altitude > cameraControls.altitude + cameraControls.playerUpperBounds)
        {
            altitude = cameraControls.altitude + cameraControls.playerUpperBounds;
        }
        base.Move(horizontalMovement, verticalMovement);
    }

    public void ResetOnRespawn()
    {
        healthManager.health = healthManager.maxHealth;
    }
}
