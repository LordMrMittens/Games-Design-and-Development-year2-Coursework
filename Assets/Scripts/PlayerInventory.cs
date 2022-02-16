using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
        ResetInventory();
        playerTransformation = GetComponent<PlayerTransformationController>();
        
    }
    public void ResetInventory()
    {
        hasFireRateUp = false;
        hasDoubleShot = false;
        hasShotPowerUp = false;
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
    }
}
