using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float destroyAfterThisTime;
    private void Start()
    {
        Destroy(gameObject, destroyAfterThisTime);
    }
}
