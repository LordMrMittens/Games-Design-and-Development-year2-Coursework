using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
    [SerializeField] float rotationSpeedX;
    [SerializeField] float rotationSpeedY;
    [SerializeField] float rotationSpeedZ;
    public bool randomizeRotation;
    public bool movesDown;
    public float moveSpeed;
    void Start()
    {
        if (randomizeRotation)
        {
            rotationSpeedX = Random.Range(-rotationSpeedX, rotationSpeedX);
            rotationSpeedY = Random.Range(-rotationSpeedY, rotationSpeedY);
            rotationSpeedZ = Random.Range(-rotationSpeedZ, rotationSpeedZ);
        }
    }
    void Update()
    {
        transform.Rotate(Vector3.right * (rotationSpeedX * Time.deltaTime));
        transform.Rotate(Vector3.up * (rotationSpeedY * Time.deltaTime));
        transform.Rotate(Vector3.forward * (rotationSpeedZ * Time.deltaTime));
        if (movesDown)
        {
            transform.Translate((Vector3.down * moveSpeed) * Time.deltaTime,Space.World);
        }
        if(transform.position.y < Camera.main.transform.position.y - 20)
        {
            Destroy(gameObject);
        }
    }
    
}
