using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject healthBar;
    public GameObject otherUI;
    public MenuChecker menuChecker;

    public AudioSource clickSource;

    void Awake () 
    {
        otherUI.SetActive(true);
        AudioListener.pause = false;
        clickSource.ignoreListenerPause = true;
        gameIsPaused = false;
        menuChecker = gameObject.GetComponent<MenuChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else if (!menuChecker.inUI)
            {
                Pause();
            }
        }
    }

    public void Resume () 
    {
        pauseMenuUI.SetActive(false);
        otherUI.SetActive(true);
        healthBar.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameIsPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause () 
    {
        pauseMenuUI.SetActive(true);
        otherUI.SetActive(false);
        healthBar.SetActive(false);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        gameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMenu ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
        //Cursor.lockState = CursorLockMode.None;
    }
}
