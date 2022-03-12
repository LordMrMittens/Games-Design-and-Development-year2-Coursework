using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] BossController bossController;
    [SerializeField] EndingSoundManager endingSound;
    [SerializeField] int totalHealth;
    public BossHealthBar healthbar;
    public int health { get; set; }
    

    private void Start()
    {
        health = totalHealth;
        healthbar.SetMaxHealth(totalHealth);
    }
    public void DamageHealth(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        if (health < 0)
        {
            GameManager.TGM.EndPhaseThree();
            bossController.EnableExplosions();
            endingSound.PlaySource(endingSound.spaceShipExplosion);
        }
    }
}
