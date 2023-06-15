using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    public void UnlockLevel(string key)
    {
        PlayerPrefs.SetInt(key, 1);
    }
}
