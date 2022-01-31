using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    PlayerMovementController playerMovementController;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject bulletTemplate;
    [SerializeField] GameObject cannon;
    [SerializeField] LayerMask targetMask;
    [SerializeField] float maxBulletSpeed;
    [SerializeField] int bulletDamage;
    int damage;
    Vector3 target;
    int direction;
    PlayerInventory playerInventory;
    float speed;
    float rotation;
    float altitude;
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerInventory = GetComponent<PlayerInventory>();
        damage = bulletDamage;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }        
    }
    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, targetMask))
        {
            ObtainShootingDirection(hit);
        }
        Vector3 targetDir = target - cannon.transform.position;
        float angle = Vector3.Angle(targetDir, cannon.transform.up);
        speed = playerMovementController.speed;
        rotation = playerMovementController.rotation;
        altitude = playerMovementController.altitude;
        GameObject Bullet = Instantiate(bulletTemplate, cannon.transform.position, Quaternion.identity);
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
        projectile.lateralSpeed = angle * direction;
    }
    private void ObtainShootingDirection(RaycastHit hit)
    {
        target = hit.point;
        if (hit.collider.name == "Left")
        {
            direction = -1;
        }
        else if (hit.collider.name == "Right")
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    }
    public void doubleDamage()
    {
        damage = bulletDamage * 2;
    }
}
