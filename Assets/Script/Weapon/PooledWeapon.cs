using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PooledWeapon : MonoBehaviour
{
    [SerializeField] private Transform bulletPosition;

    [SerializeField] private float FireRate = 10f;
    [SerializeField] public bool isFiring = false;
    public int Ammunition = 10; // jumlah peluru di magazine
    public static int Magazine; //peluru yang ada di magazine
    private int MaxedAmmo; //total peluru yang ada (peluru yang ada di magazine x 5)
    public int currentTotalAmmo; //total peluru ang ada saat itu
    public int currentMag; //total peluru saat ini
    private float lastFired;

    //reload cooldown
    public float ReloadCooldown = 2f;
    float defaultCoolDown;
    public bool Reloading;
    private Image reloadProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        Magazine = Ammunition;
        currentMag = Ammunition;

        MaxedAmmo = Ammunition * 2;
        currentTotalAmmo = MaxedAmmo;

        defaultCoolDown = ReloadCooldown;

        //reloadProgressBar = GameObject.Find("Cooldown Bar (Reloading)").GetComponent<Image>();
        //reloadProgressBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //reloadProgressBar.fillAmount = ReloadCooldown / defaultCoolDown;

        //Sistem Cooldown
        if(Reloading)
        {
            if(ReloadCooldown > 0)
            {
                ReloadCooldown -= Time.deltaTime;
            }
            else
            {
                Reloading = false;
                ReloadCooldown = defaultCoolDown;
                Reload();
            }
        }

        if(currentTotalAmmo < 0) //Agar nilai total ammo tidak dibawah 0
        {
            currentTotalAmmo = 0;
        }

        if(currentTotalAmmo > MaxedAmmo)
        {
            currentTotalAmmo = MaxedAmmo;
        }

        //bulletLeft = Ammunition - currentMag;

        if (currentMag > 0)
        {
            if (isFiring && !Reloading)
            {
                if (Time.time - lastFired > 1 / FireRate)
                {
                    lastFired = Time.time;
                    GameObject bullet = ObjectPool.instance.GetPooledObject();
                    currentMag -= 1;

                    if (bullet != null)
                    {
                        bullet.transform.position = bulletPosition.position;
                        bullet.transform.rotation = bulletPosition.rotation;
                        bullet.SetActive(true);
                    }
                }

                if (currentMag == 0 && currentTotalAmmo > 0)
                {
                    Reloading = true;
                }
                
            }
        }
    }

    public void Fire()
    {
        isFiring = true;
    }

    public void Stop()
    {
        isFiring = false;
    }

    public void Reload()
    {
        if (currentMag < Magazine)
        {
            if (currentTotalAmmo < Magazine && currentTotalAmmo > 0) //jika total amunisi dibawah nilai jumlah magazine dan tidak dibawah 0
            {
                int reloadedAmmo = Magazine - currentMag;
                if(reloadedAmmo < currentTotalAmmo)
                {
                    currentMag += reloadedAmmo;
                    currentTotalAmmo -= reloadedAmmo;
                }

                if(reloadedAmmo > currentTotalAmmo)
                {
                    currentMag = currentMag + currentTotalAmmo;
                    currentTotalAmmo = currentTotalAmmo - currentTotalAmmo;
                }
            }

            else if (currentTotalAmmo <= 0)
            {
                currentTotalAmmo = 0;
            }

            else
            {
                if (currentMag > 0)
                {
                    currentTotalAmmo = currentTotalAmmo - (Magazine - currentMag);
                    currentMag = Magazine;
                }

                else if (currentMag == 0 && currentTotalAmmo > 0)
                {
                    currentMag = Magazine;
                    currentTotalAmmo = currentTotalAmmo - currentMag;
                }
            }
        }
    }
}