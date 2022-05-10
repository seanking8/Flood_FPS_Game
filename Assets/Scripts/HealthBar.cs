using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public GameObject GameOver;
    public AudioSource gameOverSound;
    public GameObject otherUI;

    public void Start()
    {
        GameOver.gameObject.SetActive(false);
        gameOverSound.ignoreListenerPause = true;
        AudioListener.pause = false;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (health <= 0)
        {
            gameOverSound.Play();
            AudioListener.pause = true;
            Time.timeScale = 0f;
            GameOver.gameObject.SetActive(true);
            otherUI.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }
    }

    public IEnumerator Mute()
    {
        yield return new WaitForSeconds(2f);
        AudioListener.pause = true;

    }
}
