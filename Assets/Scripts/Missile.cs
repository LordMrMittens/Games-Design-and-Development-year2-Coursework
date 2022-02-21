using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : AgentMover
{
    [SerializeField] private float aliveTimer;
    bool homing;
    Transform target;
    [SerializeField]bool isPlayerMissile;
    public override void Start()
    {
        agent.enabled = true;
        Destroy(gameObject, aliveTimer);
    }
    public override void Update()
    {
        
        if (homing && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            agent.SetDestination(target.transform.position);
            if (distance < 1)
            {
                Destroy(gameObject,.5f);
            }
            //transform.LookAt(target);
        }
        if (target == null)
        {
            Destroy(gameObject);
        }
 
    }
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        
        homing = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
        {
            if (isPlayerMissile)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.GetComponent<HealthManager>().TakeDamage(100);
                    Destroy(gameObject);

                }
            } else
            {
                if(other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<HealthManager>().TakeDamage(10);
                    Destroy(gameObject);
                }
            }
        }
    }
}
