using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 20f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public float hitForce = 30f;

    public float coolDown = 0f;

    public bool underWater;

    public LayerMask ignoreMe;
    public AudioSource gunShot;
    public AudioSource UWgunShot;
    public AudioSource enemyHit1;
    public AudioSource enemyHit2;

    void Awake()
    {
        underWater = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time >= coolDown)
        {
            coolDown = Time.time + 0.5f;
            Shoot();
        }

    }

    void Shoot()
    {
        muzzleFlash.Play();

        if (underWater)
        {
            UWgunShot.Play();
        }
        else
        {
            gunShot.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~ignoreMe))
        {
            Debug.Log(hit.transform.name);

            var target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                enemyHit1 = hit.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>();
                enemyHit2 = hit.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>();

                int x = Random.Range(1, 10);

                if (x > 5)
                {
                    enemyHit1.Play();
                }
                else
                {
                    enemyHit2.Play();
                }

                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            if (hit.transform.tag == "Metal")
            {
                enemyHit1 = hit.transform.GetChild(0).GetComponent<AudioSource>();
                enemyHit2 = hit.transform.GetChild(1).GetComponentInChildren<AudioSource>();

                int x = Random.Range(1, 10);

                if (x > 5)
                {
                    enemyHit1.Play();
                }
                else
                {
                    enemyHit2.Play();
                }
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
