using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRotation : Mover
{

    //Shooting
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject bulletTemplate;
    [SerializeField]GameObject cannon;
    [SerializeField] LayerMask targetMask;
    [SerializeField] float maxBulletSpeed;
    Vector3 target;
    int direction;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rotation = 0;
        altitude = -5;
        
    }

    // Update is called once per frame
    void Update()
    {


        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, targetMask))
        {
            ObtainShootingDirection(hit);
        }
        Vector3 targetDir = target - cannon.transform.position;
        float angle = Vector3.Angle(targetDir, cannon.transform.up);
        GameObject Bullet = Instantiate(bulletTemplate, cannon.transform.position, Quaternion.identity);
        Projectiles projectile = Bullet.GetComponent<Projectiles>();
        Bullet.transform.forward = target - transform.position;
        projectile.rotation = rotation - .3f;
        projectile.altitude = altitude + .2f;
        projectile.speed = speed / Mathf.Sqrt(angle);
        if (projectile.speed > maxBulletSpeed)
        {
            projectile.speed = maxBulletSpeed;
        }
        projectile.lateralSpeed = angle * direction;
    }

    private void ObtainShootingDirection(RaycastHit hit)
    {
        target = hit.point;
        if (hit.collider.name == "Left")
        {
            direction = -1;
        }
        else if (hit.collider.name == "Right")
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    }
    public override void Move(float horizontalMovement,float verticalMovement)
    {
        if (altitude <=-5) 
        { 
            altitude = -5;
        } else if (altitude >= 5)
        {
            altitude = 5;
        }  
        base.Move(horizontalMovement,verticalMovement);
        

    }
}
