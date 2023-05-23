using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrainProjectile : MonoBehaviour
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

    public Transform BulletPoint;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject Projectile;

    public float ProjectileSpeed = 5f;
    public float ProjectileVelocity = 4f;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 5;

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

        Health = defaultHealth;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(Projectile, this.gameObject.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    void LateUpdate()
    {
        player = GameObject.Find("Player").transform;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

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

        if (Health == 0)
        {
            EnemyDeath();
        }
    }
    private void ResetAttack()
    {
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
            Spawn();
            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public void Spawn()
    {
        GameObject item = GetPooledObject();
        if (item != null)
        {
            item.transform.position = BulletPoint.transform.position;
            item.transform.rotation = BulletPoint.transform.rotation;
            item.SetActive(true);

            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.AddForce(item.transform.forward * ProjectileSpeed, ForceMode.Impulse);
            rb.AddForce(item.transform.up * ProjectileVelocity, ForceMode.Impulse);
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
        this.gameObject.SetActive(false);
    }
}
