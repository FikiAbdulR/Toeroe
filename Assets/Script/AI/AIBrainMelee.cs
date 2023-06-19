using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrainMelee : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    public LayerMask Player;

    public int Health;
    int defaultHealth;

    [Range(0, 10)] public float speed;
    [Range(1, 50)] public float walkRadius;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float timer;
    bool isPatrolling;
    public float randTime;
    float lastRandTime;

    public BoxCollider AttackBox;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public Animator AiAnim;

    private void Awake()
    {
        defaultHealth = Health;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        isPatrolling = true;
        GenerateRandInt();

        AttackBox.enabled = false;
        Health = defaultHealth;

        AiAnim.GetComponent<Animator>();
        AiAnim.SetBool("Dead", false);

    }

    void LateUpdate()
    {
        player = GameObject.Find("Player").transform;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!GameplayManager.instance.isPaused)
        {
            agent.Resume();

            if (!playerInSightRange && !playerInAttackRange)
            {
                Patrol();
                AiAnim.SetBool("Attack", false);
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                Chase();
                AiAnim.SetBool("Attack", false);
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                Attack();
                AiAnim.SetBool("Attack", true);
            }

            if (Health == 0)
            {
                EnemyDeath();
            }
        }
        else
        {
            agent.Stop();
        }
    }
    private void ResetAttack()
    {
        AttackBox.enabled = false;
        alreadyAttacked = false;
    }
    void Patrol()
    {
        if (isPatrolling)
        {
            timer += Time.deltaTime;
            if (timer > randTime)
            {
                isPatrolling = false;
                GenerateRandInt();
                timer = 0;
            }

            if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(RandomNavMeshLocation());
            }
        }
        else
        {
            agent.SetDestination(agent.transform.position);

            timer += Time.deltaTime;
            if (timer > randTime)
            {
                isPatrolling = true;
                GenerateRandInt();
                timer = 0;
            }
        }
    }

    void GenerateRandInt()
    {
        randTime = Random.Range(2, 7);
        if (randTime == lastRandTime)
        {
            randTime = Random.Range(2, 7);
        }
        lastRandTime = randTime;
    }

    void Chase()
    {
        agent.SetDestination(player.position);

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
    }

    void Attack()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            ///Attack code here
            AttackBox.enabled = true;
            SoundManagerScript.instance.Playsound(5);

            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void EnemyDeath()
    {
        AiAnim.SetBool("Dead", true);
        agent.Stop();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void resetStats()
    {
        Health = defaultHealth;
        AiAnim.SetBool("Dead", false);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
