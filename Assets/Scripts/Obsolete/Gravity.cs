using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] float gravity = -1;
    public void Attract(Transform Object)
    {
        Vector3 gravityUp = (Object.position - transform.position).normalized;
        Vector3 WhatIsUp = -Object.right;

        Object.GetComponent<Rigidbody>().AddForce(new Vector3(gravityUp.x, 0, gravityUp.z)* gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(new Vector3(0,WhatIsUp.y,0), gravityUp) * Object.rotation;
        Object.rotation = Quaternion.Slerp(Object.rotation, targetRotation, 50 * Time.deltaTime);

    }
}
