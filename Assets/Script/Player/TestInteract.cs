using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInteract : MonoBehaviour
{
    private InteractManager Int_Manager;
    public bool isInteract;

    private void Awake()
    {
        Int_Manager = new InteractManager();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInteract = Int_Manager.Interact.Reload.ReadValue<float>() > 0;

        if(isInteract)
        {
            Debug.Log("Is Interact");
        }
    }

    private void OnEnable()
    {
        Int_Manager.Enable();
    }

    private void OnDisable()
    {
        Int_Manager.Disable();
    }

    void InteractObject()
    {

    }

}
