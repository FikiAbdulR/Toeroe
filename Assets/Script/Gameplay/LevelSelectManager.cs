using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject[] Panel;
    public Button[] LockedLevel;
    private int[] lvlStatus;

    // Start is called before the first frame update
    void Start()
    {
        LevelCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelCheck()
    {
        int levelStatus2 = PlayerPrefs.GetInt("Level2");
        int levelStatus3 = PlayerPrefs.GetInt("Level3");
        int levelStatus4 = PlayerPrefs.GetInt("Level4");
        int levelStatus5 = PlayerPrefs.GetInt("Level5");

        if(levelStatus2 == 1)
        {
            LockedLevel[0].interactable = true;
            Panel[0].SetActive(true);
        }
        else
        {
            LockedLevel[0].interactable = false;
            Panel[0].SetActive(false);
        }

        if (levelStatus3 == 1)
        {
            LockedLevel[1].interactable = true;
            Panel[1].SetActive(true);
        }
        else
        {
            LockedLevel[1].interactable = false;
            Panel[1].SetActive(false);
        }

        if (levelStatus4 == 1)
        {
            LockedLevel[2].interactable = true;
            Panel[2].SetActive(true);
        }
        else
        {
            LockedLevel[2].interactable = false;
            Panel[2].SetActive(false);
        }

        if (levelStatus5 == 1)
        {
            LockedLevel[3].interactable = true;
            Panel[3].SetActive(true);
        }
        else
        {
            LockedLevel[3].interactable = false;
            Panel[3].SetActive(false);
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.DeleteKey("Level2");
        PlayerPrefs.DeleteKey("Level3");
        PlayerPrefs.DeleteKey("Level4");
        PlayerPrefs.DeleteKey("Level5");

        Panel[0].SetActive(false);
        Panel[1].SetActive(false);
        Panel[2].SetActive(false);
        Panel[3].SetActive(false);

        LevelCheck();
    }
}
