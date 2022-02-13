using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }
    private void Update()
    {
        if (healthManager.health <= 0)
        {
            GameManager.TGM.StopGame();
            //GameOver
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Bullet")
        {
            other.gameObject.SetActive(false);
            
            healthManager.TakeDamage(other.GetComponent<HealthManager>().damageGiven);
        }
    }
}
