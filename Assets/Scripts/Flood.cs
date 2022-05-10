using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Flood : MonoBehaviour
{
    public Transform target;
    public float speed;

    public float waitTime = 3f;
    private float drownTimer = 0.0f;
    private float floodTimer = 0.0f;
    public float startFloodTime = 6.0f;

    public GameObject player;
    private FirstPersonController FPSInput;

    public double waterWalkSpeed;
    public float swimSpeed;

    public Gun gunScript;

    public GameObject underwaterFX;
    public EnemyHealth enemyHealth;
    public AudioSource underWaterAmb;
    public AudioSource waterRising;
    void Start()
    {
        FPSInput = player.GetComponent<FirstPersonController>();
        waterRising = GetComponent<AudioSource>();
        underwaterFX.SetActive(false);
        swimSpeed = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floodTimer += Time.deltaTime;

        if (floodTimer > startFloodTime)
        {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.MoveTowards(a, b, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {
            Debug.Log("Body's Wet!");

            FPSInput.m_WalkSpeed = swimSpeed;
            FPSInput.m_RunSpeed = swimSpeed;
            FPSInput.legsWet = true;

        }
        else if (other.tag == "Head")
        {
            underWaterAmb.Play();
            waterRising.Pause();

            FPSInput.m_GravityMultiplier = 0.15f;
            FPSInput.m_JumpSpeed = swimSpeed;
            FPSInput.headsWet = true;

            underwaterFX.SetActive(true);
        }

        if (other.tag == "enemy")
        {
            enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.isInWater = true;
        }
        if (other.tag == "Gun")
        {
            gunScript = other.GetComponent<Gun>();
            gunScript.underWater = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Head")
        {
            drownTimer += Time.deltaTime;

            if (drownTimer > waitTime)
            {
                player.GetComponent<Health>().TakeDamage(10);
                // Remove the recorded 2 seconds.
                drownTimer = drownTimer - waitTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Body")
        {

            FPSInput.m_WalkSpeed = 5;
            FPSInput.m_RunSpeed = 10;
            FPSInput.legsWet = false;

        }
        else if (other.tag == "Head")
        {
            drownTimer = 0f;

            waterRising.Play();
            underWaterAmb.Pause();

            FPSInput.m_GravityMultiplier = 2f;
            FPSInput.m_JumpSpeed = 10;
            FPSInput.headsWet = false;

            underwaterFX.SetActive(false);
        }

        if (other.tag == "Gun")
        {
            gunScript.underWater = false;
        }

    }
}
