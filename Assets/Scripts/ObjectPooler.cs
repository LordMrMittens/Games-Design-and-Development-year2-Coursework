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

    private void Awake()
    {
        pooler = this;
    }
    
    public void Start()
    {
        CreateObjectPool(pooledPlayerBullets, playerBullets, playerBulletsToPool);
        CreateObjectPool(pooledEnemyBullets, enemyBullets, enemyBulletsToPool);
        CreateObjectPool(pooledBossBullets, bossBullets, bossBulletsToPool);
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
