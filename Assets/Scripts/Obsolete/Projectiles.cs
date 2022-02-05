using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Mover
{
    public float lateralSpeed { get; set; }
    public int damage { get; set; }
    public override void Start()
    {
        
        center = GameObject.Find("Center"); 
        origin = center.transform.position;
        Destroy(gameObject, 2); 
    }
    void Update()
    {
        Move(lateralSpeed,lateralSpeed); 
    }
    public override void Move(float horizontalMovement, float verticalMovement)
    {
        rotation -= lateralSpeed * Time.deltaTime;
        altitude += speed * Time.deltaTime;
        transform.position = origin + Quaternion.Euler(0, rotation, 0) * new Vector3(0, altitude, (center.GetComponent<CapsuleCollider>().radius + radiusOffset));
        KeepFacingCenter();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && other.tag != "PowerUp" && other.tag != "MouseMask" && other.tag != "Center")
        {
            other.GetComponent<HealthManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
