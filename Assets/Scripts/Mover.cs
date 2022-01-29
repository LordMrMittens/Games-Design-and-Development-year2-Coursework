using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public float radiusOffset;
    public float speed;
    public GameObject center;
    public Vector3 origin;
    public float rotation { get; set; }
    public float elevation;
    public float movementSpeed;
    public float horizontalMovement { get; set; }
    public float setHorizontalMovement { get; set; }
    public float verticalMovement { get; set; }
    public virtual void Start()
    {
        rotation = transform.eulerAngles.y;
        origin = center.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Move(float horizontalMovement, float verticalMovement)
    {
        rotation -= horizontalMovement * speed * Time.deltaTime; //horizontal movement
        elevation += verticalMovement * (speed / 6) * Time.deltaTime; //vertical speed should be slower than horizontal speed
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, elevation, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        //this makes the player's "back" to always face the center of the cylinder
        transform.LookAt(new Vector3(origin.x, transform.position.y, origin.z), Vector3.up);
    }


}
