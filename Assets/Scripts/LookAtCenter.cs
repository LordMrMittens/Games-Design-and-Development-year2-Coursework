using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : Mover
{
    private void Update()
    {
        transform.LookAt(new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z));
    }
}
