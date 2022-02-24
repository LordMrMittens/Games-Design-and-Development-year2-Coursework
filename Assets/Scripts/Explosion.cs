using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damageDone { get; set; }
    void Start()
    {
        Destroy(gameObject, 1.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthManager>())
        {
            other.GetComponent<HealthManager>().TakeDamage(damageDone/2);
        }
    }
}
