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
    [SerializeField] float timeBetweenEnemySpawnsPhaseOne;
    [SerializeField] float timeBetweenEnemySpawnsPhaseTwo;
    [SerializeField] float enemySpawnCounter =0;
    [SerializeField] Transform initialSpawnPoint;
    void Update()
    {
        if (GameManager.TGM.playerIsAlive)
        {
            CheckPlayerPosition();
            CheckIfSpawningIsPossible();
        }

    }
    private void CheckIfSpawningIsPossible()
    {
        if (GameManager.TGM.enemiesOnScreen < GameManager.TGM.targetEnemiesOnScreen)
        {
            enemySpawnCounter += Time.deltaTime;
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
            {
                TrySpawnEnemy(timeBetweenEnemySpawnsPhaseOne);
            } else if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
            {
                TrySpawnEnemy(timeBetweenEnemySpawnsPhaseTwo);
            }
        }
    }

    private void TrySpawnEnemy(float timeBetweenSpawns)
    {
        if (enemySpawnCounter > timeBetweenSpawns)
        {
            Spawner spawnpoint;
            int unitToSpawn;
            ChooseEnemyAndSpawnLocation(out spawnpoint, out unitToSpawn);
            SpawnEnemy(spawnpoint, unitToSpawn);
            enemySpawnCounter = 0;
        }
    }

    private void SpawnEnemy(Spawner spawnpoint, int unitToSpawn)
    {
        switch (unitToSpawn)
        {
            case 0:
                GameObject squad = Instantiate(enemySquadPrefab, initialSpawnPoint.position,Quaternion.identity);
                squad.GetComponent<EnemyMarchController>().PlaceObject(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.TGM.CountEnemyUp();
                break;
            case 1:

                GameObject kamikaze = Instantiate(enemyKamikazePrefab, initialSpawnPoint.position, Quaternion.identity);
                EnemyPatrol patrolManager = kamikaze.GetComponent<EnemyPatrol>();
                patrolManager.PlaceObject(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.TGM.CountEnemyUp();
                break;
            case 2:
                GameObject bomber = Instantiate(enemyBomberPrefab, initialSpawnPoint.position, Quaternion.identity);
                bomber.GetComponent<EnemyBomber>().PlaceObject(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.TGM.CountEnemyUp();
                break;
            case 3:
                GameObject drone = Instantiate(enemyDronePrefab, initialSpawnPoint.position, Quaternion.identity);
                EnemyDrone droneManager = drone.GetComponent<EnemyDrone>();
                droneManager.PlaceObject(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.TGM.CountEnemyUp();
                
                break;
            case 4:
                GameObject hydra = Instantiate(enemyHydraPrefab, initialSpawnPoint.position, Quaternion.identity);
                hydra.GetComponent<EnemyHydra>().PlaceObject(spawnpoint.rotation, spawnpoint.altitude);
                GameManager.TGM.CountEnemyUp();
                break;
            case 5:
                Debug.Log("Phase 3");
                break;
        }
    }
    private void ChooseEnemyAndSpawnLocation(out Spawner spawnpoint, out int unitToSpawn)
    {
        if (GameManager.TGM.playerIsAlive)
        {
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseOne)
            {
                spawnpoint = new Spawner() { rotation = (playerMovementController.rotation + 180) + Random.Range(-125, 125), altitude = playerMovementController.altitude + 5 };
                unitToSpawn = Random.Range(0, 3);
            }
            else if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
            {
                if (GameManager.TGM.playerIsAlive)
                {
                    spawnpoint = new Spawner() { rotation = playerMovementController.rotation + Random.Range(-90, 90), altitude = playerMovementController.altitude + 5 };

                    unitToSpawn = Random.Range(1, 5);
                }
                else
                {
                    spawnpoint = null;
                    unitToSpawn = 0;
                }
            }
            else
            {
                spawnpoint = null;
                unitToSpawn = 0; }
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
