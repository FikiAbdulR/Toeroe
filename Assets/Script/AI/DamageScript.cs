using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int Damage = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;

            HealthManager.instance.DamagePlayer(Damage);
        }
    }
}
