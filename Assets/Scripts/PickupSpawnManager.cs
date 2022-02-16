using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pickupType { Repair, Shield, Missile, HomingMissile, Devastator, DoubleShot, RateOfFireUp, ShotPowerUp }
public class PickupSpawnManager : MonoBehaviour
{
    PlayerInventory playerInventory;
    HealthManager healthManager;
    GameObject player;
    [SerializeField] GameObject pickupPrefab;
    public static PickupSpawnManager PSM;
    public bool pickupIsPresent = false;

    private void Start()
    {
        PSM = this;
        
    }
    private void Update()
    {
        if(player == null && GameManager.TGM.playerIsAlive)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerInventory = player.GetComponent<PlayerInventory>();
            healthManager = player.GetComponent<HealthManager>();
        }
    }

    public void DecideWhichPickUpToSpawn(Vector3 location)
    {
        if (!pickupIsPresent)
        {
            int powerUpSelection = Random.Range(0, 5);
            if (powerUpSelection == 0)
            {
                if (!playerInventory.hasDoubleShot)
                {
                    SpawnPickup(pickupType.DoubleShot, location);
                }
                else if (!playerInventory.hasFireRateUp)
                {
                    SpawnPickup(pickupType.RateOfFireUp, location);
                }
                else if (!playerInventory.hasShotPowerUp)
                {
                    SpawnPickup(pickupType.ShotPowerUp, location);
                }
                else
                {
                    SpawnPickup(pickupType.Missile, location);
                }
            } else if (powerUpSelection == 1)
            {
                if(healthManager.health != healthManager.maxHealth)
                {
                    SpawnPickup(pickupType.Repair, location);
                } else if (!healthManager.shields)
                {
                    SpawnPickup(pickupType.Shield, location);
                }
                else
                {
                    SpawnPickup(pickupType.HomingMissile, location);
                }
            } else if (powerUpSelection == 2)
            {
                SpawnPickup(pickupType.Missile, location);
            } else if (powerUpSelection == 3)
            {
                SpawnPickup(pickupType.HomingMissile, location);
            } else
            {
                SpawnPickup(pickupType.Devastator, location);
            }
        }
    }
    void SpawnPickup(pickupType type, Vector3 location)
    {
        GameObject pickUpObject = Instantiate(pickupPrefab, location, Quaternion.identity);
        PickUp pickupController = pickUpObject.GetComponent<PickUp>();
        pickupController.pickup = type;
        
    }
}
