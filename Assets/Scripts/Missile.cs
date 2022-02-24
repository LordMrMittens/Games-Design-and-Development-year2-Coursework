using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : AgentMover
{
    [SerializeField] private float aliveTimer;
    bool homing;
    Transform target;
    [SerializeField]bool isPlayerMissile;
    [SerializeField] ParticleSystem explosion;
    public Vector3 manualTargetLocation;
    Vector3 targetLastLocation;
    [SerializeField] float closeExplosionTimer;
    
    public override void Start()
    {
        agent.enabled = true;
        
        targetLastLocation = manualTargetLocation;
    }
    public override void Update()
    {
        float explosionTimer = 0;
        float lifeTimer = 0;
        lifeTimer += Time.deltaTime;
        explosionTimer += Time.deltaTime;
        if (lifeTimer > aliveTimer)
        {
            ExplodeMissile();
        }
        if (homing && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            agent.SetDestination(target.transform.position);
            if(targetLastLocation != target.transform.position)
            { targetLastLocation = manualTargetLocation; }
            if (distance < 1)
            {
                explosionTimer += Time.deltaTime;
                if (explosionTimer > closeExplosionTimer)
                {
                    ExplodeMissile();
                }
            }
            //transform.LookAt(target);
        }
        if (target == null)
        {
            ExplodeMissile();
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
                    ExplodeMissile();

                }
            } else
            {
                if(other.gameObject.tag == "Player")
                {
                    other.gameObject.GetComponent<HealthManager>().TakeDamage(10);
                    ExplodeMissile();
                }
            }
        }
    }
    public void ExplodeMissile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
