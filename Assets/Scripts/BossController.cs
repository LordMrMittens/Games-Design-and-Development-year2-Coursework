using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] BossGun[] bossGuns;
    [SerializeField] BossMissileLauncher[] missileLaunchers;
    [SerializeField] BossElectricNodes[] electricnodes;
    GameObject player;
    public float timeBetweenLasers;
    public float laserTimer { get; set; }
    public bool laserIsReady;
    public float timeBetweenBulletHell;
    public float bulletHellTimer { get; set; }
    public bool buletHellIsReady;
    public float timeBetweenMissiles;
    public float missileTimer { get; set; }
    public bool missileIsReady;
    public float timeBetweenElectric;
    public float electricTimer { get; set; }
    public bool electricIsReady;
    public float timeBetweenAttacks;
    float attackTimer;
    bool attackIsReady = false;
    [SerializeField] float rotateSpeed;
    Vector3 currentEulerAngles = new Vector3(0, 0, 0);
    [SerializeField] ParticleSystem bossExplosion;
    public float z {get;set;}
    void Start()
    {
        bossGuns = GetComponentsInChildren<BossGun>();
        missileLaunchers = GetComponentsInChildren<BossMissileLauncher>();
        electricnodes = GetComponentsInChildren<BossElectricNodes>();
        attackTimer = 0;
        laserTimer = 0;
        bulletHellTimer = 0;
        missileTimer = 0;
        electricTimer = 0;
    }
    void Update()
    {
        UpdateAttackTimers();
        TryAttack();
        currentEulerAngles += new Vector3(0, z, 0) * Time.deltaTime * rotateSpeed;
        transform.localEulerAngles = currentEulerAngles;
    }
    private void TryAttack()
    {
        if (attackIsReady == true)
        {
            int attackChosen = ChooseAttack();
            switch (attackChosen)
            {
                case 0: case 1:
                    if (laserIsReady)
                    {
                        foreach (BossGun gun in bossGuns)
                        {

                            if (gun.enabled)
                            {
                                gun.LaserAttack();
                                z = 1;
                            }
                        }
                    }
                    break;
                case 2: case 3:
                    if (buletHellIsReady)
                    {
                        foreach (BossGun gun in bossGuns)
                        {
                            if (gun.enabled)
                            {
                                gun.BulletHellAttack();
                                
                            }
                        }
                    } 
                    break;
                case 4:
                    if (missileIsReady)
                    {
                        foreach (BossMissileLauncher missileLauncher in missileLaunchers)
                        {
                            if (missileLauncher.enabled)
                            {
                                missileLauncher.FireMissiles();
                            }
                        }
                    }
                    break;
                case 5:
                    if (electricIsReady)
                    {
                        foreach (BossElectricNodes node in electricnodes)
                        {
                            if (node.enabled)
                            {
                                node.ElectricAttack();
                                electricIsReady = false;
                            }
                        }
                    }   
                    break;
            }
            attackTimer = 0;
        }
    }

    private void UpdateAttackTimers()
    {
        attackTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;
        bulletHellTimer += Time.deltaTime;
        missileTimer += Time.deltaTime;
        electricTimer += Time.deltaTime;
        if (attackTimer > timeBetweenAttacks)
        {
            attackIsReady = true;
        }
        if (laserTimer > timeBetweenLasers)
        {
            laserIsReady = true;
        }
        if (bulletHellTimer > timeBetweenBulletHell)
        {
            buletHellIsReady = true;
        }
        if (missileTimer > timeBetweenMissiles)
        {
            missileIsReady = true;
        }
        if (electricTimer > timeBetweenElectric)
        {
            electricIsReady = true;
        }
    }
    private int ChooseAttack()
    {
        int chosenAttack = Random.Range(1, 6);
        return chosenAttack;
    }
    public void EnableExplosions()
    {
        bossExplosion.gameObject.SetActive(true);
    }
    public void DeathRay()
    {

    }
}
