using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuShip : MonoBehaviour
{
    float maxRotation;
    float minRotation;
    float maxPosition;
    float minPosition;
    float Speed;
    float newPositionTimer = 0;
    float timetoChooseNewPosition = 6;

    private void Update()
    {
        float t = Time.time / 12;
        float y = Mathf.Lerp(0, 2.5f, Mathf.PingPong(t, 1));
        float z = Mathf.Lerp(250, 280, Mathf.PingPong(t /6, 1));
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        transform.rotation = Quaternion.Euler(270,z,0);
    }

}
