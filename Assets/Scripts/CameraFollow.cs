using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Mover
{
    Transform player;
    [SerializeField] float offset;
    [SerializeField] float verticalDeadZone;
    [SerializeField] float horizontalDeadZone;
    [SerializeField] float verticalOffset;
    public float playerLowerBounds;
    public float playerUpperBounds;
    PlayerMovementController playerMovementController;

    public override void Start()
    {
        base.Start();
        radiusOffset = offset;
    }
    void LateUpdate()
    {if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne || GameManager.TGM.levelPhase == GameManager.Phase.PhaseThree)
        {
            if (GameManager.TGM.playerIsAlive)
            {

                
                if (player == null && playerMovementController == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player").transform;
                    playerMovementController = player.GetComponent<PlayerMovementController>();
                    speed = playerMovementController.speed;
                }
            }
            if (GameManager.TGM.playerIsAlive && playerMovementController != null)
            {
                rotation = playerMovementController.rotation;
                altitude = playerMovementController.altitude + verticalOffset;
                Move(0, 0);
            }
        }
    if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
        {
            if (GameManager.TGM.playerIsAlive)
            {
                if (player == null && playerMovementController == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player").transform;
                    playerMovementController = player.GetComponent<PlayerMovementController>();
                    speed = playerMovementController.speed;
                    altitude = playerMovementController.altitude + verticalOffset; //set this up the same as player then camera moves up independently;
                    
                }
                rotation = playerMovementController.rotation;

                Move(0, GameManager.TGM.constantScrollingSpeed);
                

            }
        }
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        altitude += verticalMovement * (speed / 6) * Time.deltaTime;
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));

        KeepFacingCenter();
    }
}
