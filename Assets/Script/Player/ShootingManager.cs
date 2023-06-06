using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoDisplay;

    private InputManagers Inpt_Managers;
    private InteractManager Int_Manager;

    public bool isReload;
    public bool isShoot;
    bool weapon1;
    bool weapon2;
    bool weapon3;



    public PooledWeapon EquipedWeapon;

    public GameObject[] Weapons;

    // Start is called before the first frame update
    private void Awake()
    {
        Inpt_Managers = new InputManagers();
        Int_Manager = new InteractManager();
    }

    private void Start()
    {
        EquipedWeapon = Weapons[0].gameObject.GetComponentInChildren<PooledWeapon>();

        Weapons[0].SetActive(true);
        Weapons[1].SetActive(false);
        Weapons[2].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(GameplayManager.instance.isStart == true)
        {
            if (GameplayManager.instance.isEnd == false)
            {
                if (!GameplayManager.instance.isPaused)
                {
                    ammoDisplay.text = EquipedWeapon.currentMag.ToString() + "/" + EquipedWeapon.currentTotalAmmo.ToString();

                    isShoot = Inpt_Managers.Player.Shoot.ReadValue<float>() > 0;
                    isReload = Int_Manager.Interact.Reload.ReadValue<float>() > 0;

                    weapon1 = Int_Manager.Interact.Weapon1.ReadValue<float>() > 0;
                    weapon2 = Int_Manager.Interact.Weapon2.ReadValue<float>() > 0;
                    weapon3 = Int_Manager.Interact.Weapon3.ReadValue<float>() > 0;

                    if (isReload)
                    {
                        EquipedWeapon.Reloading = true;
                    }

                    if (isShoot)
                    {
                        EquipedWeapon.Fire();
                    }
                    else
                    {
                        EquipedWeapon.Stop();
                    }

                    if (weapon1)
                    {
                        Switch(0);
                    }

                    if (weapon2)
                    {
                        Switch(1);
                    }

                    if (weapon3)
                    {
                        Switch(2);
                    }
                }
            }
        }
    }

    private void Switch(int Index)
    {
        switch(Index)
        {
            case 0 :
                Weapons[0].SetActive(true);
                Weapons[1].SetActive(false);
                Weapons[2].SetActive(false);
                EquipedWeapon = Weapons[0].gameObject.GetComponentInChildren<PooledWeapon>();

                IconChange.instance.ChangeIcon(Index);
                break;
            case 1:
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(true);
                Weapons[2].SetActive(false);
                EquipedWeapon = Weapons[1].gameObject.GetComponentInChildren<PooledWeapon>();

                IconChange.instance.ChangeIcon(Index);
                break;
            case 2:
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(false);
                Weapons[2].SetActive(true);
                EquipedWeapon = Weapons[2].gameObject.GetComponentInChildren<PooledWeapon>();

                IconChange.instance.ChangeIcon(Index);
                break;
        }

    }

    private void OnEnable()
    {
        Inpt_Managers.Enable();
        Int_Manager.Enable();
    }

    private void OnDisable()
    {
        Inpt_Managers.Disable();
        Int_Manager.Disable();
    }
}
