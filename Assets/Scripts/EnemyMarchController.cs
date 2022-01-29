using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarchController : Mover
{
    [SerializeField] int timeToChangeDirection;
    [SerializeField] int verticalDistanceToTravel;
    public float startingRotation { get; set; }
    [SerializeField] float rotationOffset;
    public float startingAltitude { get; set; }
    [SerializeField] float altitudeOffset;
    public int enemiesToSpawn;
    [SerializeField] GameObject enemyPrefab;
    List<GameObject> enemiesInSquad = new List<GameObject>();
    // Start is called before the first frame update
    public override void Start()
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemiesInSquad.Add(enemy);
        }

        base.Start();
        horizontalMovement = movementSpeed;
        setHorizontalMovement = horizontalMovement;
        StartCoroutine(changeDirection());
    }
    // Update is called once per frame
    void Update()
    {
        Move(horizontalMovement, verticalMovement);
        float j = altitudeOffset;
        for (int i = 0; i < enemiesInSquad.Count; i++)
        {
            if (i % 10 == 0)
            {
                j += altitudeOffset;
            }
            enemiesInSquad[i].GetComponent<EnemyMarch>().rotation = rotation + ((i % 10) * rotationOffset);
                enemiesInSquad[i].GetComponent<EnemyMarch>().altitude = (altitude + altitudeOffset) + j;
            

            Debug.Log(j);
        }
       
    }
    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(timeToChangeDirection);
        horizontalMovement = 0;
        setHorizontalMovement *= -1;
        verticalMovement = -verticalDistanceToTravel;
        yield return new WaitForSeconds(.3f);
        verticalMovement = 0;
        horizontalMovement = setHorizontalMovement;
        if(transform.position.y > 0)
        {
            StartCoroutine(changeDirection());
        }
    }
}
