using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    [SerializeField] GameObject fullHealthCity;
    [SerializeField] GameObject midHealthCity;
    [SerializeField] GameObject lowHealthCity;
    [SerializeField] HealthManager healthManager;
    
    void Start()
    {
        
        fullHealthCity.SetActive(true);
        midHealthCity.SetActive(false);
        lowHealthCity.SetActive(false);
    }
    private void Update()
    {
        if (healthManager.health <= healthManager.maxHealth/1.43f && midHealthCity.activeSelf ==false)
        {
            fullHealthCity.SetActive(false);
            midHealthCity.SetActive(true);
            lowHealthCity.SetActive(false);
        }
        if (healthManager.health <= healthManager.maxHealth / 10 && lowHealthCity.activeSelf == false)
        {
            fullHealthCity.SetActive(false);
            midHealthCity.SetActive(false);
            lowHealthCity.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Bullet")
        {
            other.gameObject.SetActive(false);
            
            healthManager.TakeDamage(other.GetComponent<HealthManager>().damageGiven);
            GameManager.TGM.colonyHealth = GetHealth();
        }
    }
    public int GetHealth()
    {
        return healthManager.health;
    }
    public void SetHealth(int health)
    {
        healthManager.health = health;
    }
}
