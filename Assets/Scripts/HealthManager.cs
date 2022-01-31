using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }
}
