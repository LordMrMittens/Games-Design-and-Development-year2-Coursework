using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] cannons;
    public LayerMask targetMask;
    PlayerInventory playerInventory;
    public Ray ray;
    [SerializeField] float setTimeBetweenShots;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject devastatorPrefab;

    new void Start()
    {
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
        if (playerInventory.hasFireRateUp)
        {
            DubleFireRate();
            if (playerInventory.hasShotPowerUp)
            {
                DoubleDamage();
            }
            else
            {
                ResetDamage();
            }
        }
        else
        {
            ResetFireRate();
        }
   
        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            switch (playerInventory.upgrades)
            {
                case PlayerUpgrades.none:
                    Shoot(bulletType.normal);
                    
                    break;
                case PlayerUpgrades.doubleShot:
                    Shoot(bulletType.normal);
                    
                    break;
                case PlayerUpgrades.fireRate:
                    Shoot(bulletType.fast);
                    
                    break;
                case PlayerUpgrades.doubleDamage:
                    Shoot(bulletType.doublePower);
                    
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerInventory.missiles > 0)
            {
                ShootMissiles();
            }
            else
            {
                //play sounds
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerInventory.devastators > 0)
            {
                Instantiate(devastatorPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                //play sounds
            }

        }
    }
    private void OnEnable()
    {
        ResetDamage();
        ResetFireRate();
    }
    public void Shoot(bulletType type)
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
                        RotateAroundProjectile bulletManager = bullet.GetComponent<RotateAroundProjectile>();
                        bulletManager.damage = damage;
                        bulletManager.bullet = type;
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
        timeBetweenShots = setTimeBetweenShots/ 3;
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
        if (enemies.Length > 0)
        {
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
            Missile missileController = missile.GetComponent<Missile>();
            missileController.Fire(closestEnemy.transform);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, targetMask))
            {
                Vector3 target = hit.point;
                missileController.manualTargetLocation = target;
                    }
            playerInventory.missiles--;
        }

        
    }
}
