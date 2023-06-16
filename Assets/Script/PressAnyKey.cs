using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject OpeningPanel;

    // Start is called before the first frame update
    void Start()
    {
        OpeningPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            OpeningPanel.SetActive(false);
        }
    }
}
