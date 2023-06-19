using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private Transform player;
    public LayerMask Player;

    public float attackRange;
    public bool playerInAttackRange;

    public Transform Turret;
    public Transform BulletPoint;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject Projectile;

    public float ProjectileSpeed = 5f;
    public float ProjectileVelocity = 4f;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(Projectile, this.gameObject.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.Find("Player").transform;

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!GameplayManager.instance.isPaused)
        {
            if (playerInAttackRange)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        Vector3 lookPos = player.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        float eulerY = lookRot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
        Turret.transform.rotation = rotation;

        if (!alreadyAttacked)
        {
            ///Attack code here
            Spawn();
            SoundManagerScript.instance.Playsound(4);

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

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
