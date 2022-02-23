using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int totalHealth;
    public BossHealthBar healthbar;
    public int health { get; set; }
    

    private void Start()
    {

        healthbar = GetComponent<BossHealthBar>();
        health = totalHealth;
        healthbar.SetMaxHealth(totalHealth);
    }
    public void DamageHealth(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        if (health < 0)
        {
            Debug.Log("Boss destroyed");
        }
    }
}
