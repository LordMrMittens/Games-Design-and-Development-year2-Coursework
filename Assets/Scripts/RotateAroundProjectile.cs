using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundProjectile : MonoBehaviour
{
    public float speed;
    public Transform center;
    public float verticalSpeed;
    public float distanceFromCenter;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromCenter = Vector3.Distance(transform.position, center.transform.position);
        distanceFromCenter = 16;
        transform.position = (transform.position - center.transform.position).normalized * distanceFromCenter + center.transform.position;
        /*
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);*/
        //transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
        Destroy(gameObject, 2);
    }
}
