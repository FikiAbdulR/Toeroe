using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        Debug.Log("Restart");
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ChangeScene(string SceneName)
    {
        Debug.Log("Go to scene" + SceneName);
        Application.LoadLevel(SceneName.ToString());
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
