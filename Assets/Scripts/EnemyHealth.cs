using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float time;

    public ParticleSystem explosion;

    public MeshRenderer meshRenderer;
    public AudioSource explodeSound;
    public AudioSource explodeWaterSound;
    public bool isInWater;

    //public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        isInWater = false;
        //healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            //Invoke(nameof(Die), .5f);
        }

        //healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        explosion.Play();
        if (isInWater)
        {
            explodeWaterSound.Play();
        }
        else
        {
            explodeSound.Play();
        }
        meshRenderer.enabled = false;
        Destroy(gameObject, time);
    }

}
