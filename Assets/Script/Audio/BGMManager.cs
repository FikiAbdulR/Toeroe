using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource Music;
    public AudioSource Sfx;

    int AudioState;
    int SfxState;

    // Start is called before the first frame update
    void Start()
    {
        AudioState = PlayerPrefs.GetInt("AudioState");
        SfxState = PlayerPrefs.GetInt("SfxState");

        Instance = this;
        Music.GetComponent<AudioSource>();
        Sfx.GetComponent<AudioSource>();

        if(AudioState == 1)
        {
            Music.volume = 0.5f;
        }
        else if(AudioState == 0)
        {
            Music.volume = 0;
        }

        if(SfxState == 1)
        {
            Sfx.volume = 0.5f;
        }
        else if(SfxState == 0)
        {
            Sfx.volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
