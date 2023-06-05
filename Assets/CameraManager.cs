using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera[] cameraPos;

    public int CameraIndex;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos[0].Priority = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeIndexCam(int indexCam)
    {
        for (int i = 0; i < cameraPos.Length; i++)
        {
            if (i == indexCam)
            {
                cameraPos[i].Priority = 5; // Set priority to 5 for the selected camera
            }
            else
            {
                cameraPos[i].Priority = 1; // Set priority to 1 for other cameras
            }
        }
    }
}
