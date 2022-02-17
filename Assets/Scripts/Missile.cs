using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : AgentMover
{
    [SerializeField] private float aliveTimer;
    bool homing;
    Transform target;
    public override void Start()
    {
        agent.enabled = true;
    }
    public override void Update()
    {

        if (homing && target != null)
        {
            if (transform.position == target.transform.position)
            {
                Destroy(gameObject);
            }
            transform.LookAt(target);
        }
        if (target == null)
        {
            Destroy(gameObject);
        }
 
    }
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        agent.SetDestination(target.transform.position);
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
        {
            if (other.gameObject.tag =="Enemy")
            {
                other.gameObject.GetComponent<HealthManager>().TakeDamage(100);
                Destroy(gameObject);
                
            }
        }
    }
}
