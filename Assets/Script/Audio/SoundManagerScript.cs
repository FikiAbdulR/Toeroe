using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    public AudioClip[] AudioList;
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Playsound(int Index)
    {
        Source.PlayOneShot(AudioList[Index]);
    }
}
