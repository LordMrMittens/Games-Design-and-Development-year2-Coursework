using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject victoryMenu;
    bool isPaused = false;
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScreenOne()
    {
        SceneManager.LoadScene(1);
        isPaused = false;
        Time.timeScale = 0;
    }
    public void LoadSceneOne()
    {
        SceneManager.LoadScene(2);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void LoadScreenTwo()
    {
        SceneManager.LoadScene(3);
        isPaused = false;
        Time.timeScale = 0;
    }
    public void LoadSceneTwo()
    {
        SceneManager.LoadScene(4);
        GameManager.TGM.LoadPhaseTwo();
        isPaused = false;
        Time.timeScale = 1;
    }
    public void LoadScreenThree()
    {
        SceneManager.LoadScene(5);

        isPaused = false;
        Time.timeScale = 0;
    }
    public void LoadSceneThree()
    {
        SceneManager.LoadScene(6);
        GameManager.TGM.LoadPhaseThree();
        isPaused = false;
        Time.timeScale = 1;
    }
    public void QuitToMenu()
    {
        UnPause();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                if (!isPaused)
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                    isPaused = true;
                }
                else
                {
                    UnPause();
                }
            }
        }
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void VictoryMenu()
    {
        victoryMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
}
