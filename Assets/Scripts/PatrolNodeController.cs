using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNodeController : Mover
{
    CameraFollow cameraController;
    public override void Start()
    {
        base.Start();
        cameraController = Camera.main.GetComponent<CameraFollow>();
        
    }

    void Update()
    {
        if (altitude < cameraController.altitude - 1 && GameManager.TGM.levelPhase != GameManager.Phase.PhaseOne)
        {
            DestroyNode();
        }
        Move(0, 0);
    }
    void DestroyNode()
    {
        gameObject.SetActive(false);
        PatrolNodeSpawnController.PNSC.patrolNodes.Remove(this.gameObject);
        Destroy(gameObject, 1);
       
    }
}
