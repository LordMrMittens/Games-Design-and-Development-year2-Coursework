using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    
    Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = (-transform.forward * horizontalInput) + (transform.up * verticalInput);
        if(horizontalInput != 0)
        {
            playerRB.AddRelativeForce(move * movementSpeed, ForceMode.Impulse);
        }
    }
}
