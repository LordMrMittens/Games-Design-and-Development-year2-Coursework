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

    void Start()
    {
        UpdateFromGameManager();

        playerTransformation = GetComponent<PlayerTransformationController>();

    }

    private void UpdateFromGameManager()
    {
        hasDoubleShot = GameManager.TGM.playerhasDoubleShot;
        hasFireRateUp = GameManager.TGM.playerhasFireRate;
        hasShotPowerUp = GameManager.TGM.playerhasDoubleDamage;
        missiles = GameManager.TGM.missiles;
        homingMissiles = GameManager.TGM.homingMissiles;
        devastators = GameManager.TGM.Devastators;
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
            upgrades = PlayerUpgrades.doubleShot;
        }
        if (hasFireRateUp)
        {
            upgrades = PlayerUpgrades.fireRate;
        }
        if (hasShotPowerUp)
        {
            upgrades = PlayerUpgrades.doubleDamage;
        } else
        {
            upgrades = PlayerUpgrades.none;
        }
        UpdateGameManager();
    }

    private void UpdateGameManager()
    {

        GameManager.TGM.playerhasDoubleShot = hasDoubleShot;
        GameManager.TGM.playerhasFireRate = hasFireRateUp;
        GameManager.TGM.playerhasDoubleDamage = hasShotPowerUp;
        GameManager.TGM.missiles = missiles;
        GameManager.TGM.homingMissiles = homingMissiles;
        GameManager.TGM.Devastators = devastators;
    }
}
