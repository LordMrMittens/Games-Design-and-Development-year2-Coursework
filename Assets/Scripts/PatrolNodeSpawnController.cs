using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNodeSpawnController : Mover
{
    CameraFollow cameraController;
    [SerializeField] GameObject patrolNodePrefab;
    [SerializeField] float upperSpawnDistance;
    [SerializeField] float bottomSpawnDistance;
    [SerializeField] float timeBetweenSpawns;
    float spawnCounter;
    [SerializeField] int columnsToSpawn;
    [SerializeField] int rowsToSpawn;
    public List<GameObject> patrolNodes = new List<GameObject>();
    public static PatrolNodeSpawnController PNSC;
    // Start is called before the first frame update
    public override void Start()
    {
        cameraController = Camera.main.GetComponent<CameraFollow>();
        spawnCounter = 0;
        PNSC = this;
        SpawnPatrolPoints(rowsToSpawn);
    }
    void Update()
    {
        if (GameManager.TGM.playerIsAlive)
        {
            if (GameManager.TGM.levelPhase == GameManager.Phase.PhaseTwo)
            {
                spawnCounter += Time.deltaTime;
                if (spawnCounter > timeBetweenSpawns)
                {
                    SpawnPatrolPoints();
                    spawnCounter = 0;
                }
            }
        }
    }
    void SpawnPatrolPoints(int rows)
    {
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columnsToSpawn; i++)
            {
                PatrolSpawnPoint patrolSpawn = new PatrolSpawnPoint { rotation = i * (360 / columnsToSpawn), altitude = (cameraController.altitude - bottomSpawnDistance) + j};
                GameObject patrolNode = Instantiate(patrolNodePrefab);
                patrolNode.GetComponent<PatrolNodeController>().PlaceObject(patrolSpawn.rotation, patrolSpawn.altitude);
                patrolNodes.Add(patrolNode);
            }
        }

    }
    void SpawnPatrolPoints()
    {
        for (int i = 0; i < columnsToSpawn; i++)
        {
            PatrolSpawnPoint patrolSpawn = new PatrolSpawnPoint { rotation = i * (360 / columnsToSpawn), altitude = cameraController.altitude + upperSpawnDistance };
            GameObject patrolNode = Instantiate(patrolNodePrefab);
            patrolNode.GetComponent<PatrolNodeController>().PlaceObject(patrolSpawn.rotation, patrolSpawn.altitude);
            patrolNodes.Add(patrolNode);
        }
    }
}
public class PatrolSpawnPoint
{
    public float rotation;
    public float altitude;
}
