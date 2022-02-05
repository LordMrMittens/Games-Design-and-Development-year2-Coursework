using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject player;
    public GameObject bulletTemplate;
    public GameObject cannon;

    public float maxBulletSpeed;
    public int bulletDamage;
    public int damage { get; set; }
    Vector3 target;
    Transform center;
    public Vector3 origin;
    public float timeBetweenShots;
    public float shotTimer { get; set; }
    public bool canShoot { get; set; }
    void Start()
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
            Vector3 targetDir = target - cannon.transform.position;
            float angle = Vector3.Angle(targetDir, cannon.transform.right);
            GameObject Bullet = Instantiate(bulletTemplate, cannon.transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * maxBulletSpeed, ForceMode.Impulse);
            Bullet.GetComponent<RotateAroundProjectile>().damage = damage;
            shotTimer = 0;
        }
    }
}
