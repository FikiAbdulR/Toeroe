using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    Image image;
    public Sprite[] Switch;

    bool isActive;
    public int AudioState;
    public int SfxState;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        AudioState = PlayerPrefs.GetInt("AudioState");
        SfxState = PlayerPrefs.GetInt("SfxState");

        if (SfxState == 1 || SfxState == 1)
        {
            image.sprite = Switch[1];
        }
        else if(SfxState == 0 || SfxState == 0)
        {
            image.sprite = Switch[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingMusic()
    {
        isActive = !isActive;

        if (isActive)
        {
            AudioState = 1;
            PlayerPrefs.SetInt("AudioState", AudioState);
        }
        else
        {
            AudioState = 0;
            PlayerPrefs.SetInt("AudioState", AudioState);
        }

        if (AudioState == 1)
        {
            image.sprite = Switch[1];
            BGMManager.Instance.Music.volume = 0.5f;
        }
        else
        {
            image.sprite = Switch[0];
            BGMManager.Instance.Music.volume = 0;
        }
    }

    public void SettingSfx()
    {
        isActive = !isActive;

        if (isActive)
        {
            SfxState = 1;
            PlayerPrefs.SetInt("SfxState", SfxState);
        }
        else
        {
            SfxState = 0;
            PlayerPrefs.SetInt("SfxState", SfxState);
        }

        if (SfxState == 1)
        {
            image.sprite = Switch[1];
            BGMManager.Instance.Sfx.volume = 0.5f;
        }
        else
        {
            image.sprite = Switch[0];
            BGMManager.Instance.Sfx.volume = 0;
        }
    }
}
