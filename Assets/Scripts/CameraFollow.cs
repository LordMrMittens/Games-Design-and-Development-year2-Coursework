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
    PlayerMovementController playerMovementController;

    public override void Start()
    {
        base.Start();
        radiusOffset = offset;
    }
    void LateUpdate()
    {
        if (GameManager.gameManager.playerIsAlive)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (/*player != null &&*/ playerMovementController == null)
            {
                playerMovementController = player.GetComponent<PlayerMovementController>();
            }
        }
        if (GameManager.gameManager.playerIsAlive && playerMovementController != null)
        {
            rotation = playerMovementController.rotation;
            altitude = playerMovementController.altitude+verticalOffset;
            Move(0, 0);
        }
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));

        KeepFacingCenter();
    }
}
