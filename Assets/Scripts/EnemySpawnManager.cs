using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemySquadPrefab;
    int enemySquadKillCount;
    [SerializeField] GameObject enemyKamikazePrefab;
    int enemyKamikazeKillCount;
    [SerializeField] GameObject enemyBomberPrefab;
    int enemyBomberKillCount;
    [SerializeField] GameObject enemyDronePrefab;
    int enemyDroneKillCount;
    [SerializeField] GameObject enemyHydraPrefab;
    int enemyHydraKillCount;
    [SerializeField] GameObject player;
   [SerializeField] PlayerMovementController playerMovementController;
    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float enemySpawnCounter =0;
    void Update()
    {
        if (GameManager.gameManager.playerIsAlive)
        {
            CheckPlayerPosition();
            CheckIfSpawningIsPossible();
        }
    }
    private void CheckIfSpawningIsPossible()
    {
        if (GameManager.gameManager.enemiesOnScreen < GameManager.gameManager.targetEnemiesOnScreen)
        {
            enemySpawnCounter += Time.deltaTime;
            if (enemySpawnCounter > timeBetweenEnemySpawns)
            {
                Spawner spawnpoint;
                int unitToSpawn;
                ChooseEnemyAndSpawnLocation(out spawnpoint, out unitToSpawn);
                SpawnEnemy(spawnpoint, unitToSpawn);
                enemySpawnCounter = 0;
            }
        }
    }
    private void SpawnEnemy(Spawner spawnpoint, int unitToSpawn)
    {
        switch (unitToSpawn)
        {
            case 0:
                GameObject squad = Instantiate(enemySquadPrefab);
                squad.GetComponent<EnemyMarchController>().PlaceEnemy(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.gameManager.CountEnemyUp();
                break;
            case 1:

                GameObject kamikaze = Instantiate(enemyKamikazePrefab);
                EnemyPatrol patrolManager = kamikaze.GetComponent<EnemyPatrol>();
                patrolManager.PlaceEnemy(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.gameManager.CountEnemyUp();
                break;
            case 2:
                GameObject bomber = Instantiate(enemyBomberPrefab);
                bomber.GetComponent<EnemyBomber>().PlaceEnemy(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.gameManager.CountEnemyUp();
                break;
            case 3:
                GameObject drone = Instantiate(enemyDronePrefab);
                EnemyDrone droneManager = drone.GetComponent<EnemyDrone>();
                droneManager.PlaceEnemy(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.gameManager.CountEnemyUp();
                
                break;
            case 4:
                GameObject hydra = Instantiate(enemyHydraPrefab);
                hydra.GetComponent<EnemyHydra>().PlaceEnemy(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.gameManager.CountEnemyUp();
                break;
            case 5:
                Debug.Log("Phase 3");
                break;
        }
    }
    private void ChooseEnemyAndSpawnLocation(out Spawner spawnpoint, out int unitToSpawn)
    {
        if (GameManager.gameManager.playerIsAlive)
        {
            spawnpoint = new Spawner() { rotation = (playerMovementController.rotation + 180) + Random.Range(-125, 125), altitude = playerMovementController.altitude + 5 };
            if (GameManager.gameManager.levelPhase == GameManager.Phase.PhaseOne)
            {
                unitToSpawn = Random.Range(0, 3);
            }
            else if (GameManager.gameManager.levelPhase == GameManager.Phase.PhaseTwo)
            {
                unitToSpawn = Random.Range(1, 5);
            }
            else { unitToSpawn = 5; }
        }
        else
        {
            spawnpoint = new Spawner { rotation =0, altitude= 0 };
            unitToSpawn = 5;
        }
    }
    private void CheckPlayerPosition()
    {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerMovementController = player.GetComponent<PlayerMovementController>();
            }
    }
}
class Spawner
{
    public float rotation;
    public float altitude;
}
