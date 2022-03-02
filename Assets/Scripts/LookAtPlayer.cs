using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]EnemyDrone parentComponent;
    Transform player;
    private void Update()
    {
        if (player == null)
        {
            player = parentComponent.player.transform;
        }
        if (player != null)
        {
            if (parentComponent.state == State.Orbiting || parentComponent.state == State.Chasing)
            {
                transform.LookAt(player);
            }
        }
    }
}
