using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChecker : MonoBehaviour
{
    public bool inUI;

    public GameObject pauseMenu;
    public GameObject gameOverUI;
    public GameObject endGameUI;

    private Scene scene;

    void Awake()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Menu")
        {
            inUI = true;
        }
        else
        {
            inUI = false;
        }
    }

    void Update()
    {
        if (scene.name != "Menu")
        {
            if (pauseMenu.activeInHierarchy || gameOverUI.activeInHierarchy || endGameUI.activeInHierarchy)
            {
                inUI = true;
            }
            else
            {
                inUI = false;
            }
        }
    }
}
