using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerUpgrades { none, doubleShot, fireRate, doubleDamage}
public class PlayerInventory : MonoBehaviour
{
    public PlayerUpgrades upgrades;
    PlayerTransformationController playerTransformation;
  
    PlayerGun playerGun;
    public bool hasDoubleShot;
    public bool hasFireRateUp;
    public bool hasShotPowerUp;

    [SerializeField] GameObject missilePrefab;
    public int missiles;
    [SerializeField] GameObject homingMissilePrefab;
    public int homingMissiles;
    [SerializeField] GameObject devastatorPrefab;
    public int devastators;
    public bool hasShield;
    public ParticleSystem shield;
    public ParticleSystem.EmissionModule emmiter;
    void Start()
    {
        playerTransformation = GetComponent<PlayerTransformationController>();
        emmiter = shield.emission;
        UpdateFromGameManager();
    }

    private void UpdateFromGameManager()
    {
        hasDoubleShot = GameManager.TGM.playerHasDoubleShot;
        hasFireRateUp = GameManager.TGM.playerHasFireRate;
        hasShotPowerUp = GameManager.TGM.playerHasDoubleDamage;
        missiles = GameManager.TGM.missiles;
        homingMissiles = GameManager.TGM.homingMissiles;
        devastators = GameManager.TGM.devastators;
    }

    public void ResetInventory()
    {
        hasFireRateUp = false;
        hasDoubleShot = false;
        hasShotPowerUp = false;
        upgrades = PlayerUpgrades.none;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasShield)
        {
            emmiter.rateOverTime = 30;
        } else
        {
            emmiter.rateOverTime = 0;
        }
        if (hasDoubleShot && !playerTransformation.sidegunsActive)
        {
            playerTransformation.ActivateSideGuns();

        }
        else if (!hasDoubleShot)
        {
            playerTransformation.DisableSideGuns();
        }
        if (hasDoubleShot)
        {
            
            if (hasFireRateUp)
            {
                
                if (hasShotPowerUp)
                {
                    upgrades = PlayerUpgrades.doubleDamage;
                }
                else
                {
                    upgrades = PlayerUpgrades.fireRate;
                }
            } else
            {
                upgrades = PlayerUpgrades.doubleShot;
            }
        }
 else
        {
            upgrades = PlayerUpgrades.none;
        }
        UpdateGameManager();
    }

    private void UpdateGameManager()
    {

        GameManager.TGM.playerHasDoubleShot = hasDoubleShot;
        GameManager.TGM.playerHasFireRate = hasFireRateUp;
        GameManager.TGM.playerHasDoubleDamage = hasShotPowerUp;
        GameManager.TGM.missiles = missiles;
        GameManager.TGM.homingMissiles = homingMissiles;
        GameManager.TGM.devastators = devastators;
    }
}
