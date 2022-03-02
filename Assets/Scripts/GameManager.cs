using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public enum Phase { PhaseOne,PhaseTwo,PhaseThree}
    public Phase levelPhase { get; set; }
    public int score { get; set; }
    [SerializeField] MainMenuManager mainMenuManager;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text missilesText;
    [SerializeField] Text devastatorText;
    [SerializeField] Text timerText;
    public int targetScore;
    public bool playerIsAlive = false; 
    public static GameManager TGM;
    public int enemiesOnScreen { get; set; }
    public int targetEnemiesOnScreen { get; set; }
    [SerializeField] int targetEnemiesOnScreenPhaseOne;
    [SerializeField] int targetEnemiesOnScreenPhaseTwo;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float timeToSpawnPlayer;
    float playerSpawnCounter;
    public GameObject thePlayer { get; set; }
    PlayerMovementController playerMovementController;
    PlayerTransformationController playerTransformation;
    public float constantScrollingSpeed;
    public bool playerCanMove { get; set; }
    public float playerSpawnRotation { get; set; }
    public float playerSpawnAltitude { get; set; }

    public bool playerHasDoubleShot;
    public bool playerHasFireRate;
    public bool playerHasDoubleDamage;
    public int missiles;
    public int homingMissiles;
    public int devastators;
    public int lives;
    [SerializeField] float phaseThreeTimer;
    float phaseThreeTimeRemaning;
    int colonyHealth;
    CityController city;
    void Start()
    {
        TGM = this;
        DontDestroyOnLoad(this.gameObject);
        playerIsAlive = false;
        playerCanMove = true;
        levelPhase = Phase.PhaseOne;
        lives = 10;
    }
    
    void Update()
    {
        livesText.text = " X " + lives.ToString();
        missilesText.text = " X " + missiles.ToString();
        devastatorText.text = " X " + devastators.ToString();

        scoreText.text = "Score: " + score.ToString();
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(EndPhaseOneWait());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            
            LoadPhaseThree();
        }
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
            KeepTime();
            if(city == null)
            {
                city = FindObjectOfType<CityController>();
                city.SetHealth(colonyHealth);
                Debug.Log("DoingThis");
            }
            if (thePlayer == null)
            {
                SpawnPlayer(0, 7.5f);
            }
            targetEnemiesOnScreen = targetEnemiesOnScreenPhaseTwo;

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

    private void KeepTime()
    {
        if (phaseThreeTimeRemaning > 0)
        {
            phaseThreeTimeRemaning -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(phaseThreeTimeRemaning / 60);
            float seconds = Mathf.FloorToInt(phaseThreeTimeRemaning % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            BossDeathLaser.FindObjectOfType<BossDeathLaser>().ShootLaser();
           
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
        playerTransformation = thePlayer.GetComponent<PlayerTransformationController>();
        playerMovementController.PlaceObject(pRotation,pAltitude);
    }
    private void EndPhaseOne()
    {
        targetEnemiesOnScreen = -10000;
        colonyHealth = FindObjectOfType<CityController>().GetHealth();
        if (enemiesOnScreen <= 0)
        {
            StartCoroutine(EndPhaseOneWait());
        }
    }
    public void EndPhaseTwo()
    {
        targetEnemiesOnScreen = -10000;

            StartCoroutine(EndPhaseTwoWait());
        
    }
    public void EndPhaseThree()
    {
        targetEnemiesOnScreen = -10000;

            StartCoroutine(EndPhaseThreeWait());
        
    }
    IEnumerator EndPhaseOneWait()
    {
        playerCanMove = false;

        yield return new WaitForSeconds(1);
        playerTransformation.TransformIntoShip();
        yield return new WaitForSeconds(2);
        playerMovementController.verticalMovement = constantScrollingSpeed * 10;
    }
    IEnumerator EndPhaseTwoWait()
    {
        
        playerCanMove = false;
        yield return new WaitForSeconds(1);
        CameraFollow camera = FindObjectOfType<CameraFollow>();
        camera.cameraIsFrozen = true;
        playerMovementController.verticalMovement = constantScrollingSpeed * 10;
        yield return new WaitForSeconds(2);
        
    }
    IEnumerator EndPhaseThreeWait()
    {
        yield return new WaitForSeconds(5);
        mainMenuManager.VictoryMenu();
    }
    public void LoadPhaseOne()
    {
        timerText.gameObject.SetActive(false);
        levelPhase = Phase.PhaseOne;
    }
    public void LoadPhaseTwo()
    {
        timerText.gameObject.SetActive(false);
        Destroy(thePlayer);
        SceneManager.LoadScene("Phase2");
        OnLevelWasLoaded();
        levelPhase = Phase.PhaseTwo;
        playerCanMove = true;
        
    }
    public void LoadPhaseThree()
    {
        timerText.gameObject.SetActive(true);
        playerIsAlive = false;
        Destroy(thePlayer);
        SceneManager.LoadScene("Phase3");
        OnLevelWasLoaded();
        levelPhase = Phase.PhaseThree;
        phaseThreeTimeRemaning = phaseThreeTimer;
        playerCanMove = true;
        
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    private void OnLevelWasLoaded()
    {
        DynamicGI.UpdateEnvironment();
    }
    public void TakeLifeAway()
    {
        lives--;
        if (lives < 0)
        {
            StopGame();
            mainMenuManager.GameOverMenu();
        }
    }
    public void DisplayVictoryMenu()
    {
        StopGame();
        mainMenuManager.VictoryMenu();
    }
    public void GameOver()
    {
        StopGame();
        mainMenuManager.GameOverMenu();
    }
}

