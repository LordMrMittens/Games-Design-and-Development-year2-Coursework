using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devastator : MonoBehaviour
{
    float size =1;
    float speed = 10;
    Vector3 sizeScale;
    [SerializeField]float timeActive;
    void Start()
    {
        StartCoroutine(EndDevastator());
    }
    void Update()
    {
        size += (Time.deltaTime *speed);
        sizeScale.x = size;
        sizeScale.y = size;
        sizeScale.z = size;
        transform.localScale = sizeScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<HealthManager>().TakeDamage(30);
        }
    }
    IEnumerator EndDevastator()
    {
        yield return new WaitForSeconds(timeActive);
        Destroy(gameObject);
    }
}
