using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : Mover
{
    private void Update()
    {
        transform.LookAt(Vector3.down, Vector3.up);
    }
}
