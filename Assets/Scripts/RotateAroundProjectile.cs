using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum bulletType { normal, fast , doublePower, enemy}
public class RotateAroundProjectile : MonoBehaviour
{
    public bulletType bullet;
    public float speed;
    public Transform center;
    public float verticalSpeed;
    public float distanceFromCenter;
    public CapsuleCollider centerCollider;
    public Rigidbody RB;
    public int damage;
    public float radiusOffset;
    public bool isPlayerBullet;
    public Light pointLight;
    public ParticleSystem ps;
    [SerializeField] ParticleSystem explosion;
    void Start()
    {
        center = GameObject.Find("Center").transform;
        centerCollider = center.GetComponent<CapsuleCollider>();
        RB = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterSeconds());
        var main = ps.main;
        switch (bullet)
        {
            case bulletType.normal:
                pointLight.color = Color.green;
                main.startColor = Color.green;
                break;
            case bulletType.fast:
                pointLight.color = Color.blue;
                main.startColor = Color.blue;
                break;
            case bulletType.doublePower:
                pointLight.color = Color.yellow;
                main.startColor = Color.yellow;
                break;
            case bulletType.enemy:
                pointLight.color = Color.red;
                main.startColor = Color.red;
                break;
        }
    }
    void Update()
    {
        distanceFromCenter = centerCollider.radius +radiusOffset;
        transform.position = (transform.position - new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z)).normalized * distanceFromCenter + new Vector3(center.transform.position.x, transform.position.y, center.transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (!isPlayerBullet)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<HealthManager>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
        
        } else
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<HealthManager>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
        } 
        if(other.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator DeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (!isPlayerBullet)
        {
            bullet = bulletType.enemy;
        }
        if (RB != null)
        {
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
        }
    }
}
