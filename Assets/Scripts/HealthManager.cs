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
    public bool shields;
    public bool isBossComponent;
    BossHealth bossHealth;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] GameObject devastator;
    public bool isBomb;

    private void Start()
    {
        ResetHealth();
        if (isBossComponent)
        {
            bossHealth = GetComponentInParent<BossHealth>();
        }

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
        if (shields)
        {
            shields = false;
            inventory.hasShield = false;
        }
        else
        {
            health -= damage;
            if (isBossComponent)
            {
                bossHealth.DamageHealth(damage);
            }
            if (health <= 0)
            {
                DestroyThisObject();
            }
        }
    }
    private void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - 5 && gameObject.tag != "City")
        {
            Destroy(gameObject);
        }
    }
    public void DestroyThisObject()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (gameObject.tag == "Enemy")
        {
            if (isBossComponent)
            {
                gameObject.SetActive(false);

            }
            else
            {
                if (gameObject.GetComponent<EnemyHydra>() != null)
                {
                    gameObject.GetComponent<EnemyHydra>().DivideOnDeath();
                }
                if (gameObject.name != "Enemy(Clone)")
                {
                    if (!isBomb)
                    {
                        GameManager.TGM.CountEnemyDown();
                    }
                    Destroy(gameObject);
                }
            }
            if (Random.Range(0, 10) == 1 && !isBomb)
            {

                PickupSpawnManager.PSM.DecideWhichPickUpToSpawn(transform.position);
            }
            GameManager.TGM.score += pointsValue;
        }
        if (gameObject.tag == "Player")
        {
            GameManager.TGM.playerIsAlive = false;
            GameManager.TGM.TakeLifeAway();
            GameManager.TGM.playerSpawnAltitude = gameObject.GetComponent<PlayerMovementController>().altitude;
            GameManager.TGM.playerSpawnRotation = gameObject.GetComponent<PlayerMovementController>().rotation;
            gameObject.SetActive(false);
            Instantiate(devastator, transform.position, Quaternion.identity);
        }
        if (gameObject.tag == "City")
        {
            GameManager.TGM.GameOver();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "City" || other.tag != "Bullet")
        {
            if (other.tag == "Player")
            {
                other.GetComponent<HealthManager>().TakeDamage(damageGiven);
            }
            if (destroyOnTouch)
            {
                DestroyThisObject();
            }
        }

    }
}