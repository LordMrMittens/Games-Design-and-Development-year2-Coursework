using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundProjectile : MonoBehaviour
{
    public float speed;
    public Transform center;
    public float verticalSpeed;
    public float distanceFromCenter;
    public CapsuleCollider centerCollider;
    public Rigidbody RB;
    public int damage;
    public float radiusOffset;
    public bool isPlayerBullet;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center").transform;
        centerCollider = center.GetComponent<CapsuleCollider>();
        RB = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterSeconds());
    }
    // Update is called once per frame
    void Update()
    {
        distanceFromCenter = centerCollider.radius +radiusOffset;
        transform.position = (transform.position - new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z)).normalized * distanceFromCenter + new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z);
        //Destroy(gameObject, 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerBullet)
        {
            if (other.tag == "Player")
            {

                other.GetComponent<HealthManager>().TakeDamage(damage);
                
                // Destroy(gameObject);
                gameObject.SetActive(false);
            }
        } else
        {
            if (other.tag == "Enemy")
            {
                
                other.GetComponent<HealthManager>().TakeDamage(damage);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
