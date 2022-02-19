using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileLauncher : MonoBehaviour
{
    public GameObject player;
    public GameObject missilePrefab;
    public GameObject[] cannons;

    public float maxMissileSpeed;
    public int missileDamage;
    public int damage { get; set; }
    Transform center;
    public Vector3 origin;
    public float timeBetweenShots;
    public float shotTimer { get; set; }
    public bool canShoot { get; set; }

    public void ShootMissiles()
    {

            foreach (GameObject cannon in cannons)
            {
                GameObject missile = Instantiate(missilePrefab, cannon.transform.position, Quaternion.identity);
                missile.GetComponent<Missile>().Fire(player.transform);
            }
    }
    private void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer > timeBetweenShots)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null&&canShoot)
        {
            ShootMissiles();
            canShoot = false;
            shotTimer = 0;
        }
    }
}
