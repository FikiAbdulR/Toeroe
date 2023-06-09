using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public int Medkit = 50;

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
        if (col.gameObject.CompareTag("Player"))
        {
            if(HealthManager.instance.HealthPoint < HealthManager.instance.DefaultHealth)
            {
                HealthManager.instance.RestoreHealth(Medkit);
                this.gameObject.SetActive(false);
            }
        }
    }
}
