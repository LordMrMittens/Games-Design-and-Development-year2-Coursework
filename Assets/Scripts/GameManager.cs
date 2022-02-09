using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Phase { PhaseOne,PhaseTwo,PhaseThree}
    public Phase levelPhase;
    public int score;
    public int targetScore;
    Transform spawnPoint;
    public bool playerIsAlive = false;
    public static GameManager gameManager;
    public int enemiesOnScreen;
    public int targetEnemiesOnScreen;
    EnemySpawnManager enemySpawnManager;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float timeToSpawnPlayer;
    float playerSpawnCounter;
    GameObject thePlayer;
    PlayerMovementController playerMovementController;
    [SerializeField] float constantScrollingSpeed;
    
    void Start()
    {
        gameManager = this;
        SpawnPlayer();
        playerMovementController = thePlayer.GetComponent<PlayerMovementController>();
        levelPhase = Phase.PhaseTwo;
    }
    void Update()
    {
        if (levelPhase == Phase.PhaseOne)
        {
            if (score > targetScore)
            {
                EndPhaseOne();
                
            }
        } else if (levelPhase == Phase.PhaseTwo)
        {
            playerMovementController.verticalMovement = constantScrollingSpeed;
        } else if (levelPhase == Phase.PhaseThree)
        {

        }
        if (playerIsAlive == false)
        {
            playerSpawnCounter += Time.deltaTime;
            if (playerSpawnCounter > timeToSpawnPlayer)
            {
                playerMovementController.ResetOnRespawn();
                playerMovementController.rotation = 0;
                playerMovementController.altitude = 0;
                thePlayer.SetActive(true);
                playerSpawnCounter = 0;
                playerIsAlive = true;
            }
        }
    }
    public void CountEnemyUp()
    {
        enemiesOnScreen++;
    }
    public void CountEnemyDown()
    {
        enemiesOnScreen--;
    }
    private void SpawnPlayer()
    {
        thePlayer = Instantiate(playerPrefab);
    }
    private void EndPhaseOne()
    {
        targetEnemiesOnScreen = -10000;
        if (enemiesOnScreen <= 0)
        {
            StartCoroutine(EndPhaseOneWait());
        }
    }
    IEnumerator EndPhaseOneWait()
    {
        yield return new WaitForSeconds(5);
        //logic to prepare for phase2
        //save spawnpoint
        //save score
        //set target enemies on screen
        levelPhase = Phase.PhaseTwo;
    }
}
