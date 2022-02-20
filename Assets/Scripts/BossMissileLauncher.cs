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
    public float rotationSpeed ;

    Vector3 currentEulerAngles = new Vector3 (0,0,180);
    float z;
    bool launcherIsClosed;
    bool launcherIsOpen;
    public void ShootMissiles()
    {

            foreach (GameObject cannon in cannons)
            {
                GameObject missile = Instantiate(missilePrefab, cannon.transform.position, Quaternion.identity);
                missile.GetComponent<Missile>().Fire(player.transform);
            }
    }
    private void Start()
    {
        StartCoroutine(ShootingSequence());
    }
    private void Update()
    {

        currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = currentEulerAngles;
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


            
            canShoot = false;
            shotTimer = 0;
        }
    }
    IEnumerator ShootingSequence()
    {
            z = 1;
            yield return new WaitUntil(() => currentEulerAngles.z >= 270);
            z = 0;
            yield return new WaitForSeconds(1);
            ShootMissiles();
            yield return new WaitForSeconds(1);
            z = -1;
            yield return new WaitUntil(() => currentEulerAngles.z <= 180);
            z = 0;
    }
}
