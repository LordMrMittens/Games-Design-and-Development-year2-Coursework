using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Colliding with player");
            other.GetComponent<HealthManager>().TakeDamage(10000);
            Destroy(gameObject);
        }
    }
}
