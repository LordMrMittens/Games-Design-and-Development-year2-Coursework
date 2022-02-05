using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Bullet")
        {
            Destroy(other.gameObject);
            healthManager.TakeDamage(5);
        }
    }
}
