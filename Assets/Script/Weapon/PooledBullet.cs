using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBullet : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Rigidbody rb;

    public int bulletDamage = 10;

    public static float knockBackStrength = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        else if (hit.gameObject.CompareTag("Enemy"))
        {
            AIBrainMelee melee = hit.gameObject.GetComponent<AIBrainMelee>();
            AIBrainProjectile projectile = hit.gameObject.GetComponent<AIBrainProjectile>();

            if (melee != null)
            {
                hit.gameObject.GetComponent<AIBrainMelee>().Health -= bulletDamage;
            }

            if (projectile != null)
            {
                hit.gameObject.GetComponent<AIBrainProjectile>().Health -= bulletDamage;
            }

            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
