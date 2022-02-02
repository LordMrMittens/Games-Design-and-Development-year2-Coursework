using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    [SerializeField] int damageGiven;
    [SerializeField] bool destroyOnTouch;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy();
        }
    }
    private void Destroy()
    {
        if (gameObject.name == "EnemyHydra")
        {
            gameObject.GetComponent<EnemyHydra>().DivideOnDeath();
        }
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name != "EnemyHydra"&& other.tag=="Enemy;")
        {
            other.GetComponent<HealthManager>().TakeDamage(damageGiven);
        }
        if (destroyOnTouch)
        {
            gameObject.SetActive(false);
        }
    }
}
