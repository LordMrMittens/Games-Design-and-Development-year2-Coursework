using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarch : Mover
{
    [SerializeField] HealthManager healthManager;
    private void Update()
    {
        Move(0,0);
    }

    public override void Move(float horizontalMovement, float verticalMovement)
    {
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));

        KeepFacingCenter();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "city")
        {
            Debug.Log("Trying to damage city" + other.name);
            other.GetComponent<HealthManager>().TakeDamage(healthManager.damageGiven);
            healthManager.DestroyThisObject();
        }
    }
}
