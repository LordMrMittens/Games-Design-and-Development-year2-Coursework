using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public float radiusOffset;
    public float speed;
    public GameObject center { get; set; }
    public Vector3 origin;
    public float rotation { get; set; }
    public float altitude { get; set; }
    public float movementSpeed;
    public float horizontalMovement { get; set; }
    public float setHorizontalMovement { get; set; }
    public float verticalMovement { get; set; }
    public virtual void Start()
    {
        center = GameObject.Find("Center");
        //rotation = transform.eulerAngles.y;
        origin = center.transform.position;

    }
    public void PlaceEnemy(float startRotation, float startAltitude)
    {
        rotation = startRotation;
        altitude = startAltitude;
       // Move(0, 0);
    }
    public virtual void Move(float horizontalMovement, float verticalMovement)
    {
        rotation -= horizontalMovement * speed * Time.deltaTime;
        altitude += verticalMovement * (speed / 6) * Time.deltaTime; 
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        KeepFacingCenter();
    }
    public virtual void KeepFacingCenter()
    {
        transform.LookAt(new Vector3(origin.x, transform.position.y, origin.z), Vector3.up);
    }
}
