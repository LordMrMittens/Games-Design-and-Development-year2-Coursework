using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasShield { get; set; }
    public int shieldHealth { get; set; }
    PlayerTransformationController playerTransformation;
    public bool hasDoubleShot;
    public bool hasFireRateUp { get; set; }
    public bool hasShotPowerUp; //{ get; set; }

    [SerializeField] GameObject missilePrefab;
    int missiles;
    [SerializeField] GameObject homingMissilePrefab;
    int homingMissiles;
    [SerializeField] GameObject devastatorPrefab;
    int devastators;
    // Start is called before the first frame update
    void Start()
    {
        hasDoubleShot = false;
        playerTransformation = GetComponent<PlayerTransformationController>();
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
