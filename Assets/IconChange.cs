using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public static IconChange instance;

    private Image bulletIcon;
    [SerializeField] private Sprite[] Icons;


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
    }

    public void ChangeIcon(int Index)
    {
        switch (Index)
        {
            case 0:
                bulletIcon.sprite = Icons[Index];
                break;
            case 1:
                bulletIcon.sprite = Icons[Index];
                break;
            case 2:
                bulletIcon.sprite = Icons[Index];
                break;
        }
    }
}
