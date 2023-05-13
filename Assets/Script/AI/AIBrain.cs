using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    public LayerMask playerLayer;

    [Range(0, 10)] public float speed;
    [Range(0, 10)] public float walkRadius;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float timer;
    bool isPatrolling;
    public float randTime;
    float lastRandTime;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        isPatrolling = true;
        GenerateRandInt();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.Find("Player").transform;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            Attack();
        }
    }

    private void ResetAttack()
    {
        //reset attack here
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
    }

    void Attack()
    {
        Vector3 lookPos = player.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        float eulerY = lookRot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, eulerY, 0);

        agent.SetDestination(transform.position);
        agent.gameObject.transform.rotation = rotation;

        if (!alreadyAttacked)
        {
            ///Attack code here

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
        agent.Stop();
    }
}
