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
                squad.GetComponent<EnemyMarchController>().rotation = spawnpoint.rotation;
                squad.GetComponent<EnemyMarchController>().altitude = spawnpoint.altitude;
                break;
            case 1:
                GameObject kamikaze = Instantiate(enemyKamikazePrefab);
                EnemyPatrol patrolManager = kamikaze.GetComponent<EnemyPatrol>();
                patrolManager.rotation = spawnpoint.rotation;
                patrolManager.altitude = spawnpoint.altitude;
                break;
            case 2:
                GameObject bomber = Instantiate(enemyBomberPrefab);
                bomber.GetComponent<EnemyBomber>().rotation = spawnpoint.rotation;
                bomber.GetComponent<EnemyBomber>().altitude = spawnpoint.altitude;
                break;
            case 3:
                GameObject drone = Instantiate(enemyDronePrefab);
                EnemyDrone droneManager = drone.GetComponent<EnemyDrone>();
                droneManager.rotation = spawnpoint.rotation;
                droneManager.altitude = spawnpoint.altitude;
                break;
            case 4:
                GameObject hydra = Instantiate(enemyHydraPrefab);
                hydra.GetComponent<EnemyHydra>().rotation = spawnpoint.rotation;
                hydra.GetComponent<EnemyHydra>().altitude = spawnpoint.altitude;
                break;
        }
    }

    private void ChooseEnemyAndSpawnLocation(out Spawner spawnpoint, out int unitToSpawn)
    {
        spawnpoint = new Spawner() { rotation = playerMovementController.rotation + Random.Range(-20, 20), altitude = playerMovementController.altitude + 5 };
        unitToSpawn = Random.Range(0, 5);
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
