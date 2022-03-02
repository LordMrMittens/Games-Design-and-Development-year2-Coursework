using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float destroyAfterTime;
    public int damageDone { get; set; }
    void Start()
    {
        Destroy(gameObject, destroyAfterTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthManager>())
        {
            other.GetComponent<HealthManager>().TakeDamage(damageDone/2);
        }
    }
}
