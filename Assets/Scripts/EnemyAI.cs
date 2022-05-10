using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public AudioSource flying;
    public AudioSource attacking;


    //public Animator anim;

    public float health;
    public int damage;

    //Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public Rigidbody enemyRB;
    //public Vector3 bounceForce = new Vector3(0, 0, 0);
    public float bounceForce;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
        flying = gameObject.transform.GetChild(0).GetChild(2).GetComponent<AudioSource>();
        attacking = gameObject.transform.GetChild(0).GetChild(3).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            agent.SetDestination(transform.position);
            transform.Rotate(0f, 500f * Time.deltaTime, 0f, Space.Self);
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (attacking.isPlaying) attacking.Pause();
        if (!flying.isPlaying) flying.Play();

        if (!walkPointSet) SearchWalkpoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkpoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        if (flying.isPlaying) flying.Pause();
        if (!attacking.isPlaying) attacking.Play();

        agent.SetDestination(player.position);
        transform.Rotate(0f, 500f * Time.deltaTime, 0f, Space.Self);

        // attacking.Play();
        // if (flying.isPlaying) flying.Pause();
    }

    // void OnCollisionEnter (Collision collision)
    // {
    //     Debug.Log(collision.transform.tag);
    //     if (collision.transform.tag == "Player")
    //     {
    //         //agent.SetDestination(transform.position);
    //         player.GetComponent<Health>().TakeDamage(damage);
    //         //Rigidbody playerRB = collision.rigidbody;
    //         enemyRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
    //     }
    // }

    private void AttackPlayer()
    {
        if (flying.isPlaying) flying.Pause();
        if (!attacking.isPlaying) attacking.Play();
        
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here

            player.GetComponent<Health>().TakeDamage(damage);
            //enemyRB.AddForce(bounceForce, ForceMode.Impulse);

            //player.GetComponent<Rigidbody>().AddForce(-transform.forward * 500, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // public void TakeDamage(int damage)
    // {
    //     health -= damage;

    //     if (health <= 0)
    //     {
    //         Invoke(nameof(DestroyEnemy), .5f);
    //     }
    // }

    // private void DestroyEnemy()
    // {
    //     if (!explosion.isPlaying) explosion.Play();
    //     //explosion.Play();
    //     //Destroy(gameObject);
    // }
}
