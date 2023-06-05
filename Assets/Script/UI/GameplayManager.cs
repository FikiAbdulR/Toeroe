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

    public GameObject PausePanel;
    public GameObject RoundClear;
    public GameObject StageClear;
    public GameObject GameOver;

    public bool isPaused = true;
    public bool isEnd = false;

    private void Awake()
    {
        instance = this;
        Int_Manager = new InteractManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
        RoundClear.SetActive(false);
        StageClear.SetActive(false);
        GameOver.SetActive(false);

        isEnd = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd == false)
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
}
