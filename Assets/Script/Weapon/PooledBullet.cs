using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBullet : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Rigidbody rb;

    public static float bulletDamage = 2f;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
    }
}
