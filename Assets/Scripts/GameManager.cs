using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum Phase { PhaseOne,PhaseTwo,PhaseThree}
    public Phase levelPhase { get; set; }
    public int score { get; set; }
    public int targetScore;
    public bool playerIsAlive = false; 
    public static GameManager gameManager;
    public int enemiesOnScreen { get; set; }
    public int targetEnemiesOnScreen { get; set; }
    [SerializeField] int targetEnemiesOnScreenPhaseOne;
    [SerializeField] int targetEnemiesOnScreenPhaseTwo;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float timeToSpawnPlayer;
    float playerSpawnCounter;
    GameObject thePlayer;
    PlayerMovementController playerMovementController;
    [SerializeField] float constantScrollingSpeed;
    public bool playerCanMove { get; set; }
    public float playerSpawnRotation { get; set; }
    public float playerSpawnAltitude { get; set; }
   
    
    void Start()
    {
        gameManager = this;
        
        playerIsAlive = false;
        playerCanMove = true;
        levelPhase = Phase.PhaseOne;
       
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (levelPhase == Phase.PhaseOne)
        {
            if (thePlayer == null)
            {
                SpawnPlayer(0, -5);
            }
            if (score > targetScore)
            {
                EndPhaseOne();
            }
            else
            {
                targetEnemiesOnScreen = targetEnemiesOnScreenPhaseOne;
            }
        } else if (levelPhase == Phase.PhaseTwo)
        {
            if (thePlayer == null)
            {
                SpawnPlayer(0, -5);
            }
            targetEnemiesOnScreen = targetEnemiesOnScreenPhaseTwo;
            playerMovementController.verticalMovement = constantScrollingSpeed;
        } else if (levelPhase == Phase.PhaseThree)
        {

        }
        if (playerIsAlive == false)
        {
            playerSpawnCounter += Time.deltaTime;
            if (playerSpawnCounter > timeToSpawnPlayer)
            {
                
                playerMovementController.ResetOnRespawn();
                playerMovementController.rotation = playerSpawnRotation;
                playerMovementController.altitude = playerSpawnAltitude;
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
    private void SpawnPlayer(float pRotation, float pAltitude)
    {
        thePlayer = Instantiate(playerPrefab);
        playerMovementController = thePlayer.GetComponent<PlayerMovementController>();
        playerMovementController.PlaceEnemy(pRotation,pAltitude);
    }
    private void EndPhaseOne()
    {
        targetEnemiesOnScreen = -10000;
        if (enemiesOnScreen <= 0)
        {
            StartCoroutine(EndPhaseOneWait());
        }
    }
    IEnumerator EndPhaseOneWait()
    {
        playerCanMove = false;

        yield return new WaitForSeconds(2);
        playerMovementController.verticalMovement = constantScrollingSpeed * 5;
        
        //logic to prepare for phase2
        //save score
        
        
    }
    public void LoadPhaseOne()
    {
        levelPhase = Phase.PhaseOne;
    }
    public void LoadPhaseTwo()
    {
        Destroy(thePlayer);
        SceneManager.LoadScene("Phase2");
        
        levelPhase = Phase.PhaseTwo;
        playerCanMove = true;
    }
    public void LoadPhaseThree()
    {
        levelPhase = Phase.PhaseThree;
    }
    
}

