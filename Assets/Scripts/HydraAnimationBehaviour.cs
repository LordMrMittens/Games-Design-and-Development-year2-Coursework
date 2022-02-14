using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraAnimationBehaviour : Mover
{
    [SerializeField] float timeBetweenMovements;
    [SerializeField] float rotationSpeed;
    float movementCounter;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        movementCounter = timeBetweenMovements;
    }

    // Update is called once per frame
    void Update()
    {
        MoveErratically();
    }
    private void MoveErratically()
    {
        movementCounter -= Time.deltaTime;
        if (movementCounter < 0)
        {
            transform.localScale = new Vector3( Random.Range(.15f, .19f), Random.Range(.15f, .19f), Random.Range(.15f, .19f));
            transform.Rotate(Vector3.right*(Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime));
            movementCounter = timeBetweenMovements;
        }
        
    }
}
