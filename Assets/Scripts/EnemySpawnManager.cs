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
            int unitToSpawn = Random.Range(0, 5);
            Debug.Log(unitToSpawn);
            switch (unitToSpawn)
            {
                case 0:
                    SpawnEnemy(enemyBomberPrefab);
                    break;
                case 1:
                    SpawnEnemy(enemyKamikazePrefab);
                    break;
                case 2:
                    SpawnEnemy(enemyBomberPrefab);
                    break;
                case 3:
                    SpawnEnemy(enemyDronePrefab);
                    break;
                case 4:
                    SpawnEnemy(enemyHydraPrefab);
                    break;
            }
            

        }

    }

    private void SpawnEnemy(GameObject enemy)
    {
        Spawner spawnpoint = new Spawner() { rotation = playerMovementController.rotation + 180, altitude = playerMovementController.altitude + 10 };
        GameObject squad = Instantiate(enemy);
        squad.GetComponent<EnemyMarchController>().rotation = spawnpoint.rotation;
        squad.GetComponent<EnemyMarchController>().altitude = spawnpoint.altitude;
    }
}
class Spawner
{
    public float rotation;
    public float altitude;
}
