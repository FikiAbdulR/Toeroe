using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public static IconChange instance;

    private Image bulletIcon;
    public Color[] colorIndicator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        bulletIcon = gameObject.GetComponent<Image>();
        bulletIcon.color = colorIndicator[0];
    }

    public void ChangeIcon(int Index)
    {
        bulletIcon.color = colorIndicator[Index];
    }
}
