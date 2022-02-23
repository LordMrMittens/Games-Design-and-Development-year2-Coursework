using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    bool isPaused = false;
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadSceneOne()
    {
        SceneManager.LoadScene(1);
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
}
