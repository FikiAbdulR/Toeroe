using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int DefaultHealth = 100;
    public int HealthPoint;
    public Slider HealthBar;

    private void Awake()
    {
        instance = this;
        HealthPoint = DefaultHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = DefaultHealth;
        HealthBar.value = HealthPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthBar.value = HealthPoint;

        if(HealthPoint <= 0)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Game Over");
        }
    }

    public void DamagePlayer(int DamageInput)
    {
        HealthPoint -= DamageInput;
        Debug.Log("Damaged");
    }

    public void RestoreHealth(int HealthInput)
    {
        HealthPoint += HealthInput;
        Debug.Log("HealthRestored");

        if (HealthPoint > DefaultHealth)
        {
            HealthPoint = DefaultHealth;
        }
    }
}
