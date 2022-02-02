using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject bulletTemplate;
    [SerializeField] GameObject cannon;
    
    [SerializeField] float maxBulletSpeed;
    [SerializeField] int bulletDamage;
    int damage;
    Vector3 target;
    Transform center;
    public Vector3 origin;
    public float timeBetweenShots;
    float shotTimer;
    bool canShoot;
    void Start()
    {
        player = GameObject.Find("Player");
        damage = bulletDamage;
        center = GameObject.Find("Center").transform;
        origin = center.transform.position;
        shotTimer = 0;
        canShoot = false;
    }
    private void Update()
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
    public void Shoot()
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
