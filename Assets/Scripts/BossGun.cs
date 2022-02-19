using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : Gun
{
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        base.Update();
        if (player != null)
        {
            transform.LookAt(player.transform);
            Shoot();
        }
        Debug.Log(canShoot);
    }
}
