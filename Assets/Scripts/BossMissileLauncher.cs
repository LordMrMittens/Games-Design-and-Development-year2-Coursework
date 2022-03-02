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
    public Vector3 origin;
    public float timeBetweenShots;
    public float shotTimer { get; set; }
    public bool canShoot { get; set; }
    public float rotationSpeed ;
    Vector3 currentEulerAngles = new Vector3 (0,0,180);
    float z;
    BossController bossController;
    public void ShootMissiles()
    {

            foreach (GameObject cannon in cannons)
            {
                GameObject missile = Instantiate(missilePrefab, cannon.transform.position, Quaternion.identity);
                missile.GetComponent<Missile>().Fire(player.transform);
            }
            canShoot = false;
        
    }
    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }
    public void FireMissiles()
    {
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(ShootingSequence());
        }
    }
    private void Update()
    {
        currentEulerAngles += new Vector3(0, 0, z) * Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = currentEulerAngles;
        shotTimer += Time.deltaTime;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    IEnumerator ShootingSequence()
    {
        bossController.missileTimer = -1000;
        bossController.missileIsReady = false;
        z = 1;
            yield return new WaitUntil(() => currentEulerAngles.z >= 270);
            z = 0;
        canShoot = true;
            yield return new WaitForSeconds(1);
        if (gameObject.activeInHierarchy == false)
        { yield break; }
            if (canShoot)
        {
            ShootMissiles();
        }
            yield return new WaitForSeconds(1);
            z = -1;
            yield return new WaitUntil(() => currentEulerAngles.z <= 180);
            z = 0;
        bossController.missileTimer = 0;
        bossController.missileIsReady = false;
        
    }
}
