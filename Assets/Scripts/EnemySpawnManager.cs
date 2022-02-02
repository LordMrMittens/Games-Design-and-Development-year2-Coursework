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
    GameObject player;
    PlayerMovementController playerMovementController;
    int enemiesOnScreen;
    int targetEnemiesOnScreen;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)// && gameManager.playerIsAlive)
        {
            player = GameObject.Find("Player");
            playerMovementController = player.GetComponent<PlayerMovementController>();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawner spawnpoint = new Spawner() { rotation = playerMovementController.rotation + 180, altitude = playerMovementController.altitude + 10 };
            int unitToSpawn = Random.Range(0, 5);
            Debug.Log(unitToSpawn);
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
    }
}
class Spawner
{
    public float rotation;
    public float altitude;
}
