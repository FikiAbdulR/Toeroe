using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseObject : MonoBehaviour
{
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete()
    {
        Enemy.gameObject.SetActive(false);
    }

    public void DeathSound()
    {
        SoundManagerScript.instance.Playsound(2);
    }
}
