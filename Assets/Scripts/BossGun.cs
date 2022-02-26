using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : Gun
{
    [SerializeField] Transform laserTarget;
    [SerializeField] Transform[] bulletHellTargets;
    BossController bossController;

    [SerializeField] float laserDuration;
    float laserDurationTimer;
    public override void Start()
    {
        bossController = GetComponentInParent<BossController>();
        base.Start();
    }
    public void BulletHellAttack()
    {
        StartCoroutine(bulletHell());
    }
    public void LaserAttack()
    {
        laserDurationTimer = 0;
        StartCoroutine(LaserSequence());
        
    }
    public override void Update()
    {
        laserDurationTimer += Time.deltaTime;
    }
    public void Shoot(Transform target)
    {
        GameObject bullet = ObjectPooler.pooler.GetPooledObject(ObjectPooler.pooler.pooledBossBullets);
            if (bullet != null)
            {
                bullet.transform.position = cannon.transform.position;
                bullet.transform.rotation = cannon.transform.rotation;
                bullet.SetActive(true);
                RotateAroundProjectile bulletManager = bullet.GetComponent<RotateAroundProjectile>();
                bullet.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * maxBulletSpeed, ForceMode.Impulse);
                bulletManager.damage = damage;
            }
    }
    IEnumerator bulletHell()
    {
        bossController.bulletHellTimer = -1000;
        bossController.buletHellIsReady = false;
        bossController.laserTimer = -0;
        bossController.laserIsReady = false;
        for (int i = 0; i < 20; i++)
        {
            foreach (Transform target in bulletHellTargets)
            {
                Shoot(target);
            }
            yield return new WaitForSeconds(.2f);
        }
        bossController.bulletHellTimer = 0;
        bossController.buletHellIsReady = false;
        
    }
    IEnumerator LaserSequence()
    {
        bossController.laserTimer = -10000;
        bossController.laserIsReady = false;
        bossController.bulletHellTimer = -0;
        bossController.buletHellIsReady = false;
        
        while (laserDurationTimer < laserDuration)
        {
            Shoot(laserTarget);

            yield return new WaitForSeconds(.05f);
        }
        bossController.z = 0;
        bossController.laserTimer = 0;
        bossController.laserIsReady = false;
    }
}
