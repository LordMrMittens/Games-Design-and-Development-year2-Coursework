using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler pooler;
    public List<GameObject> pooledPlayerBullets = new List<GameObject>();
    public GameObject playerBullets;
    public int playerBulletsToPool;
    public List<GameObject> pooledEnemyBullets = new List<GameObject>();
    public GameObject enemyBullets;
    public int enemyBulletsToPool;
    public List<GameObject> pooledBossBullets = new List<GameObject>();
    public GameObject bossBullets;
    public int bossBulletsToPool;
    /*    public List<GameObject> pooledEnemySquads = new List<GameObject>();
        public GameObject enemySquad;
        public int enemySquadsToPool;
        public List<GameObject> pooledEnemyKamikazes = new List<GameObject>();
        public GameObject enemyKamikaze;
        public int enemyKamikazesToPool;
        public List<GameObject> pooledEnemyBombers = new List<GameObject>();
        public GameObject enemyBomber;
        public int enemyBombersToPool;
        public List<GameObject> pooledEnemyDrones = new List<GameObject>();
        public GameObject enemyDrone;
        public int enemyDronesToPool;
        public List<GameObject> pooledEnemyHydras = new List<GameObject>();
        public GameObject enemyHydra;
        public int enemyHydrasToPool;*/


    private void Awake()
    {
        pooler = this;
    }
    
    public void Start()
    {
        CreateObjectPool(pooledPlayerBullets, playerBullets, playerBulletsToPool);
        CreateObjectPool(pooledEnemyBullets, enemyBullets, enemyBulletsToPool);
        CreateObjectPool(pooledBossBullets, bossBullets, bossBulletsToPool);
        //CreateObjectPool(pooledEnemySquads, enemySquad,enemySquadsToPool);
        //CreateObjectPool(pooledEnemyKamikazes, enemyKamikaze,enemyKamikazesToPool);
        //CreateObjectPool(pooledEnemyBombers, enemyBomber,enemyBombersToPool);
        // CreateObjectPool(pooledEnemyDrones, enemyDrone,enemyDronesToPool);
        //CreateObjectPool(pooledEnemyHydras, enemyHydra, enemyHydrasToPool);
    }

    private void CreateObjectPool(List<GameObject> listOfObjects, GameObject prefab, int numberToPool)
    {
        GameObject tmp;
        for (int i = 0; i < numberToPool; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            listOfObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject(List<GameObject> listOfObjects)
    {
        for (int i = 0; i < listOfObjects.Count; i++)
        {
            if (!listOfObjects[i].activeInHierarchy)
            {
                return listOfObjects[i];
            } 
        }
 
        return null;
    }
}
