using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    PlayerMovementController playerMovementController;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask targetMask;
    PlayerInventory playerInventory;
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerInventory = GetComponent<PlayerInventory>();
        damage = bulletDamage;
        canShoot = true;
        shotTimer = 0;
        mainCamera = Camera.main;
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.Mouse0)&&canShoot)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, targetMask))
        {
            Vector3 target = hit.point;
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
            canShoot = false;
        }
        shotTimer = 0;
    }
    public void doubleDamage()
    {
        damage = bulletDamage * 2;
    }
}
