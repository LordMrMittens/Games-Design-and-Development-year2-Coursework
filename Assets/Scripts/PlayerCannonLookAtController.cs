using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannonLookAtController : MonoBehaviour
{
    PlayerGun playerGun;
    PlayerMovementController playerController;
    public bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        playerGun = GetComponentInParent<PlayerGun>();
        playerController = GetComponentInParent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(playerGun.ray, out hit, playerGun.targetMask))
        {
            transform.LookAt(hit.point, Vector3.right);
        }
        
    }

}
