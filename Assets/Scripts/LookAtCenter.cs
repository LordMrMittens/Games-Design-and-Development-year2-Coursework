using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : Mover
{
    private void Update()
    {
        transform.localRotation = transform.parent.gameObject.transform.rotation;
    }
}
