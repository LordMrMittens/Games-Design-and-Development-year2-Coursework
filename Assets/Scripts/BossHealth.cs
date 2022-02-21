using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int totalHealth;
    public int health { get; set; }

    private void Start()
    {
        health = totalHealth;
    }
    public void DamageHealth(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Debug.Log("Boss destroyed");
        }
    }
}
