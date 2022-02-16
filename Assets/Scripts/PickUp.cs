using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : MonoBehaviour
{
    public pickupType pickup;


    public float moveSpeed;
    public float timeActive;
    float destructionConter;
    private void Start()
    {
        destructionConter = 0;
       PickupSpawnManager.PSM.pickupIsPresent = true;
    }
    void Update()
    {
        if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne && transform.position.y > 0)
        {
            transform.Translate((Vector3.down * moveSpeed) * Time.deltaTime, Space.World);
        }
        destructionConter += Time.deltaTime;
        if (destructionConter > timeActive)
        {
            DestroyPickUp();
        }
    }

    private void DestroyPickUp()
    {
        PickupSpawnManager.PSM.pickupIsPresent = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            HealthManager healthManager = other.GetComponent<HealthManager>();
            PlayerGun playerGun = other.GetComponent<PlayerGun>();
            switch (pickup)
            {
                case pickupType.Repair:
                    healthManager.health = healthManager.maxHealth;
                    break;
                case pickupType.Shield:
                    healthManager.shields = true;
                    break;
                case pickupType.Devastator:
                    playerInventory.devastators++;
                    break;
                case pickupType.Missile:
                    playerInventory.missiles += 5;
                    break;
                case pickupType.HomingMissile:
                    playerInventory.homingMissiles += 2;
                    break;
                case pickupType.DoubleShot:
                    playerInventory.hasDoubleShot = true;
                    break;
                case pickupType.RateOfFireUp:
                    playerInventory.hasFireRateUp = true;
                    playerGun.DubleFireRate();
                    break;
                case pickupType.ShotPowerUp:
                    playerInventory.hasShotPowerUp = true;
                    playerGun.DoubleDamage();
                    break;
            }
            DestroyPickUp();
        }
    }
}
