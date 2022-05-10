using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public GameObject endGameUI;
    
    public AudioSource win;

    void Awake() 
    {
        endGameUI.SetActive(false);
        win.ignoreListenerPause = true;
    }
    void OnTriggerEnter()
    {
        CompleteLevel();
    }

    void CompleteLevel()
    {
        AudioListener.pause = true;
        endGameUI.SetActive(true);
        win.PlayDelayed(1f);
    }
}
