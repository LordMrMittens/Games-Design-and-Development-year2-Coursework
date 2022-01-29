using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarch : Mover
{
    private void Update()
    {
        Move(0,0);
    }

    public override void Move(float horizontalMovement, float verticalMovement)
    {
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        
        transform.LookAt(new Vector3(origin.x, transform.position.y, origin.z), Vector3.up);
    }
}
