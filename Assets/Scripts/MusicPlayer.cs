using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource source;
    public GameObject canvas;
    public MenuChecker menuChecker;
    private bool musicIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        menuChecker = canvas.GetComponent<MenuChecker>();
        //PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuChecker.inUI == true && source.isPlaying)
        {
            PauseMusic();
        }
        else if (menuChecker.inUI == false && source.isPlaying == false)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        source.Play();
    }
    void PauseMusic()
    {
        source.Pause();
    }
}
