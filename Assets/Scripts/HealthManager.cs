using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int damageGiven;
    [SerializeField] bool destroyOnTouch;
    [SerializeField] int pointsValue;
    //GameManager gameManager;
    private void Start()
    {
        ResetHealth();
    }
    private void OnEnable()
    {
        ResetHealth();
    }
    private void ResetHealth()
    {
        health = maxHealth;
        if (gameObject.tag == "Player")
        {
            GameManager.TGM.playerIsAlive = true;
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
        GameManager.TGM.score += pointsValue;
        if (gameObject.tag == "Player")
        {
            GameManager.TGM.playerIsAlive = false;
            GameManager.TGM.playerSpawnAltitude = gameObject.GetComponent<PlayerMovementController>().altitude;
            GameManager.TGM.playerSpawnRotation = gameObject.GetComponent<PlayerMovementController>().rotation;
            gameObject.SetActive(false);
        }
        else if (gameObject.name != "Enemy(Clone)" && gameObject.name != "Bomb(Clone)")
        {
            GameManager.TGM.CountEnemyDown();
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        } 
    }
     void OnTriggerEnter(Collider other)
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

        if (transform.position.y < Camera.main.transform.position.y - 20)
        {
            Destroy(gameObject);
        }
    }
}
