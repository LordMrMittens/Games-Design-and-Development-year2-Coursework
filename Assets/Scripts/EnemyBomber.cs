using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : AgentMover
{
    enum State { Bombing, Chasing };
    State bomberState = State.Bombing;
    [SerializeField] int numberOfBombs;
    [SerializeField] float timeBetweenBombDrops;
    float bombCounter;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject minePrefab;
    bool hasBombs = true;
    public override void Start()
    {
        agent.enabled = false;
        base.Start();
        horizontalMovement = movementSpeed;
        bombCounter = timeBetweenBombDrops;
        if (Random.Range(0, 2)!=0)
        {
            horizontalMovement *= -1;
        }
    }
    // Update is called once per frame
    public override void Update()
    {
        if (bomberState == State.Bombing)
        {
            Move(horizontalMovement, verticalMovement);
            bombCounter -= Time.deltaTime;
            if (bombCounter <= 0 && hasBombs)
            {
                if (numberOfBombs > 0)
                {
                    if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
                    {
                        GameObject mine = Instantiate(minePrefab, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.identity);
                        mine.GetComponent<EnemyMine>().PlaceObject(rotation, altitude);
                        bombCounter = timeBetweenBombDrops;
                        numberOfBombs--;
                    }
                    else
                    {
                        Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.identity);
                        bombCounter = timeBetweenBombDrops;
                        numberOfBombs--;
                    }
                }
                else
                {
                   
                    bomberState = State.Chasing;
                }
            }
        }
        else
        {
            base.Update();
        }
    }
}
