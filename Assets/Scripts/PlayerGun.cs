using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    PlayerMovementController playerMovementController;
    PlayerTransformationController playerTransformation; 
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] cannons;
    public LayerMask targetMask;
    PlayerInventory playerInventory;
    public Ray ray;
    [SerializeField] float setTimeBetweenShots;
    [SerializeField] GameObject missilePrefab;
    
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerTransformation = GetComponent<PlayerTransformationController>();
        playerInventory = GetComponent<PlayerInventory>();
        cannons = GameObject.FindGameObjectsWithTag("Cannon");
        damage = bulletDamage;
        canShoot = true;
        timeBetweenShots = setTimeBetweenShots;
        shotTimer = 0;
        mainCamera = Camera.main;
    }
    public override void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        base.Update();
        if (Input.GetKey(KeyCode.Mouse0)&&canShoot)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            ShootMissiles();
        }

    }
    private void OnEnable()
    {
        ResetDamage();
        ResetFireRate();
    }
    public override void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, targetMask))
        {
            Vector3 target = hit.point;
            foreach (GameObject cannon in cannons)
            {
                if (cannon.gameObject.activeInHierarchy)
                {
                    Vector3 targetDir = target - cannon.transform.position;
                    float angle = Vector3.Angle(targetDir, cannon.transform.right);
                    GameObject bullet = ObjectPooler.pooler.GetPooledObject(ObjectPooler.pooler.pooledPlayerBullets);
                    if (bullet != null)
                    {
                        bullet.transform.position = cannon.transform.position;
                        bullet.transform.rotation = cannon.transform.rotation;
                        bullet.SetActive(true);
                        bullet.GetComponent<Rigidbody>().AddForce((target - transform.position).normalized * maxBulletSpeed, ForceMode.Impulse);
                        bullet.GetComponent<RotateAroundProjectile>().damage = damage;
                    }
                }
            }
            canShoot = false;
        }
        shotTimer = 0;
    }
    public void DoubleDamage()
    {
        damage = bulletDamage * 2;
    }
    public void ResetDamage()
    {
        damage = bulletDamage;
    }
    public void DubleFireRate()
    {
        timeBetweenShots  /= 3;
    }
    public void ResetFireRate()
    {
        timeBetweenShots = setTimeBetweenShots;
    }

    public void ShootMissiles()
    {
        float shortestDistance = 1000;
        float distance;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance > distance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        GameObject missile = Instantiate(missilePrefab, cannon.transform.position, Quaternion.identity);
        missile.GetComponent<Missile>().Fire(closestEnemy.transform);
    }
}
