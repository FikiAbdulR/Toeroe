using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    private InteractManager Int_Manager;

    public GameObject briefPanel;
    public GameObject PausePanel;
    public GameObject RoundClear;
    public GameObject StageClear;
    public GameObject GameOver;
    public GameObject Setting;

    public bool isStart = false;
    public bool isPaused = true;
    public bool isEnd = false;

    public string levelUnlocked;

    private void Awake()
    {
        instance = this;
        Int_Manager = new InteractManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        briefPanel.SetActive(true);
        PausePanel.SetActive(false);
        RoundClear.SetActive(false);
        StageClear.SetActive(false);
        GameOver.SetActive(false);
        Setting.SetActive(false);

        isStart = false;
        isEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart == true)
        {
            Time.timeScale = 1;

            if (isEnd == false)
            {
                if (Int_Manager.Interact.Paused.triggered)
                {
                    isPaused = !isPaused;

                    if (!isPaused)
                    {
                        //isPaused = true;
                        PausePanel.SetActive(false);
                        Time.timeScale = 1f;
                    }
                    else
                    {
                        //isPaused = false;
                        PausePanel.SetActive(true);
                        Time.timeScale = 0f;
                    }
                }
            }
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        briefPanel.SetActive(false);
        isStart = true;
    }

    public void ClearRound(int Rcount, bool Alpha)
    {
        if (Alpha == true)
        {
            RoundClear.SetActive(true);
            RoundClear.transform.GetComponentInChildren<TMP_Text>().text = "Round " + Rcount + " Clear".ToString();
        }
        else if (Alpha == false)
        {
            RoundClear.SetActive(false);
        }
    }

    public void Winning()
    {
        StageClear.SetActive(true);
        isEnd = true;
        Time.timeScale = 0f;

        if(levelUnlocked != null)
        {
            UnlockLevel(levelUnlocked);
        }
    }

    public void Lose()
    {
        GameOver.SetActive(true);
        isEnd = true;
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        Int_Manager.Enable();
    }

    private void OnDisable()
    {
        Int_Manager.Disable();
    }

    public void UnlockLevel(string key)
    {
        PlayerPrefs.SetInt(key, 1);
    }
}
