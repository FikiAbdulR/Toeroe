using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;
    public GameObject AlmanacPanel;

    // Start is called before the first frame update
    void Start()
    {
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        AlmanacPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
