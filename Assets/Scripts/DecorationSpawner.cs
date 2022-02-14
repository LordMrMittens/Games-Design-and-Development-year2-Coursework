using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrefabs;
    [SerializeField] GameObject[] sprites;
    [SerializeField] float maxAreaToSpawnX;
    [SerializeField] float minAreaToSpawnX;
    [SerializeField] float maxAreaToSpawnZ;
    [SerializeField] float minAreaToSpawnZ;
    [SerializeField] float timeBetweenAsteroidSpawns;
    [SerializeField] float timeBetweenSpriteSpawns;
    float asteroidSpawnCounter;
    float spriteSpawnCounter;
    
    void Start()
    {
        asteroidSpawnCounter = 0;
        spriteSpawnCounter=0;
    }

    // Update is called once per frame
    void Update()
    {
        asteroidSpawnCounter += Time.deltaTime;
        spriteSpawnCounter += Time.deltaTime;
        if (asteroidSpawnCounter > timeBetweenAsteroidSpawns)
        {
            asteroidSpawnCounter = 0;
            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);
            asteroid.transform.position = new Vector3(Random.Range(minAreaToSpawnX, maxAreaToSpawnX), Camera.main.transform.position.y + 30, Random.Range(minAreaToSpawnZ, maxAreaToSpawnZ));
            asteroid.transform.localScale = new Vector3(Random.Range(.2f, .5f), Random.Range(.2f, .5f), Random.Range(.2f, .5f));

        }
        if (asteroidSpawnCounter > timeBetweenSpriteSpawns)
        {
            GameObject sprite = Instantiate(sprites[Random.Range(0, sprites.Length)]);
            spriteSpawnCounter = 0;
        }
    }
}
