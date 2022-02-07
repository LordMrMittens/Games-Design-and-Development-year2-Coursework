using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    void Start()
    {
        gameManager = this;
        SpawnPlayer();
        playerMovementController = thePlayer.GetComponent<PlayerMovementController>();
    }
    void Update()
    {
        if (score > targetScore)
        {
            EndPhaseOne();
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
            Debug.Log("Phase 1 Won");
        }
    }
}
