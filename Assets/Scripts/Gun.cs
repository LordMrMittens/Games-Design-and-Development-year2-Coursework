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
    int direction;
    Transform center;
    float speed;
    float rotation;
    float altitude;
    public Vector3 origin;
    void Start()
    {
        player = GameObject.Find("Player");
        damage = bulletDamage;
        center = GameObject.Find("Center").transform;
        origin = center.transform.position;
    }
    void Update()
    {
       Shoot();



        Debug.Log(transform.rotation.x);
    }
    private void Shoot()
    {

            ObtainShootingDirection(player.transform);
        
        Vector3 targetDir = target - cannon.transform.position;
        float angle = Vector3.Angle(targetDir, cannon.transform.right);
        GameObject Bullet = Instantiate(bulletTemplate, cannon.transform.position, Quaternion.identity);
        Bullet.GetComponent<Rigidbody>().AddForce((player.transform.position -transform.position ).normalized*10,ForceMode.Impulse);
       // Bullet.GetComponent<RotateAroundProjectile>().verticalSpeed = -(cannon.transform.position.y - player.transform.position.y);
       // Bullet.GetComponent<RotateAroundProjectile>().verticalSpeed = -(cannon.transform.position.y - player.transform.position.y);
        /*
        Projectiles projectile = Bullet.GetComponent<Projectiles>();
        projectile.damage = damage;
        Bullet.transform.forward = target - transform.position;
        projectile.rotation = rotation - .3f;
        projectile.altitude = altitude + .2f;
        projectile.speed = speed / Mathf.Sqrt(angle);
        if (projectile.speed > maxBulletSpeed)
        {
            projectile.speed = maxBulletSpeed;
        }
        projectile.lateralSpeed = angle * direction;*/
    }
    private void ObtainShootingDirection(Transform target)
    {

    }
    public void doubleDamage()
    {
        damage = bulletDamage * 2;
    }
}
