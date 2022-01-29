using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Mover
{
    public float lateralSpeed { get; set; }
    // Start is called before the first frame update
    public override void Start()
    {
        center = GameObject.Find("Center"); 
        origin = center.transform.position;
        Destroy(gameObject, 2); 
    }
    // Update is called once per frame
    void Update()
    {
        Move(lateralSpeed,lateralSpeed); 
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        rotation -= lateralSpeed * Time.deltaTime;
        altitude += speed * Time.deltaTime;
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        transform.LookAt(new Vector3(origin.x, transform.position.y, origin.z), Vector3.up);
    }

}
