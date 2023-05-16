using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int HealthPoint = 100;
    public Slider HealthBar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = HealthPoint;
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
}
