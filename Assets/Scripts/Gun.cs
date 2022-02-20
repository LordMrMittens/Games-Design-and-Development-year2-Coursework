using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletTemplate;
    public GameObject cannon;

    public float maxBulletSpeed;
    public int bulletDamage;
    public int damage { get; set; }
    Transform center;
    public Vector3 origin;
    public float timeBetweenShots;
    public float shotTimer { get; set; }
    public bool canShoot { get; set; }
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = bulletDamage;
        center = GameObject.Find("Center").transform;
        origin = center.transform.position;
        shotTimer = 0;
        canShoot = false;
    }
    public virtual void Update()
    {
        shotTimer+=Time.deltaTime;
            if (shotTimer > timeBetweenShots)
        {
            canShoot = true;
        }
        else { 
            canShoot = false;
        }
    }
    public virtual void Shoot()
    {
        if (canShoot)
        {
            GameObject bullet = ObjectPooler.pooler.GetPooledObject(ObjectPooler.pooler.pooledEnemyBullets);
            if (bullet != null)
            {
                bullet.transform.position = cannon.transform.position;
                bullet.transform.rotation = cannon.transform.rotation;
                bullet.SetActive(true);
                RotateAroundProjectile bulletManager = bullet.GetComponent<RotateAroundProjectile>();
                bullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * maxBulletSpeed, ForceMode.Impulse);
                bulletManager.damage = damage;
                bulletManager.bullet = bulletType.enemy;
            }
            shotTimer = 0;
        }
    }
}
