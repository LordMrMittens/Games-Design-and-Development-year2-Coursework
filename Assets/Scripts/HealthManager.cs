using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int health { get; set; }
    [SerializeField] int damageGiven;
    [SerializeField] bool destroyOnTouch;
    [SerializeField] int pointsValue;
    //GameManager gameManager;
    private void Start()
    {
        health = maxHealth;
        if (gameObject.tag == "Player")
        {
            GameManager.gameManager.playerIsAlive = true;
        }
        else if(gameObject.tag == "Enemy")
        {
            if (gameObject.name != "Enemy(Clone)")
            {
                GameManager.gameManager.enemiesOnScreen++;
            }
        }
    }
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
        if (gameObject.GetComponent<EnemyHydra>() != null)
        {
            gameObject.GetComponent<EnemyHydra>().DivideOnDeath();
        }
        GameManager.gameManager.score += pointsValue;
        if (gameObject.tag == "Player")
        {
            GameManager.gameManager.playerIsAlive = false;

        }
        else
        {
            if (gameObject.name != "Enemy(Clone)")
            {
                GameManager.gameManager.enemiesOnScreen--;
            }
            
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "City"||other.tag!="Bullet")
        {
            if (other.tag == "Player")
            {
                other.GetComponent<HealthManager>().TakeDamage(damageGiven);
            }
            if (destroyOnTouch)
            {
                Destroy();
            }
        }
    }
}
