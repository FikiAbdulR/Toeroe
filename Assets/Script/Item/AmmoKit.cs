using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKit : MonoBehaviour
{
    [SerializeField] private int AddAmmo = 10;

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
            if (col.transform.GetComponent<ShootingManager>().EquipedWeapon.currentTotalAmmo < col.transform.GetComponent<ShootingManager>().EquipedWeapon.MaxedAmmo)
            {
                col.transform.GetComponent<ShootingManager>().EquipedWeapon.currentTotalAmmo += AddAmmo;
                this.gameObject.SetActive(false);
            }
        }
    }
}
