using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundProjectile : MonoBehaviour
{
    public float speed;
    public Transform center;
    public float verticalSpeed;
    public float distanceFromCenter;
    public CapsuleCollider centerRigidbody;
    public int damage;
    public float radiusOffset;
    public bool isPlayerBullet;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center").transform;
        centerRigidbody = center.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromCenter = centerRigidbody.radius +radiusOffset;
        transform.position = (transform.position - new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z)).normalized * distanceFromCenter + new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z);
        Destroy(gameObject, 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerBullet)
        {
            if (other.tag == "Player")
            {

                other.GetComponent<HealthManager>().TakeDamage(damage);
                Destroy(gameObject);
            }
        } else
        {
            if (other.tag == "Enemy")
            {
                
                other.GetComponent<HealthManager>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
