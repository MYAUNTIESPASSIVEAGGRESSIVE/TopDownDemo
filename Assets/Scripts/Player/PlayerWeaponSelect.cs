using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("References")]
    public GameObject GunHolder;
    public GameObject MeleeHolder;
    public GameObject ProjHolder;
    public PlayerControl PlayerScript;

    //Weapon Lists
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Regular Guns")]
    public List<SO_RegularGuns> Guns = new List<SO_RegularGuns> ();
    public List<int> GunAmmo = new List<int>();
    public List<int> GunCurrentClip = new List<int>();


    [Header("Projectile Weapons")]
    public List<SO_Projectile> Projectiles = new List<SO_Projectile>();
    public List<int> ProjAmmo = new List<int>();
    public List<int> ProjCurrentClip = new List<int>();


    [Header("Weapon Inv Variables")]
    //Gun Inv
    public int MaxGunsInInv = 5;
    public int GunsHolding = 0;
    //Projectile Inv
    public int MaxProjInInv = 5;
    public int ProjectileHolding = 0;
    //Melee inv
    public int MaxMeleeInInv = 5;
    public int MeleeHolding = 0;


    [Header("Weapon ID Stats")]
    public int CurrentGunID = -1;
    public int CurrentMeleeID = -1;
    public int CurrentProjID = -1;

    [Header("Other Gun Variables")]
    public int ActiveGuns;

    private void Start()
    {
        GunsHolding = GunHolder.transform.childCount;
        ProjectileHolding = ProjHolder.transform.childCount;
        MeleeHolding = MeleeHolder.transform.childCount;



        for (int i = 0; i < GunsHolding; i++)
        {
            GunHolder.transform.GetChild(i).gameObject.SetActive(false);
            GunAmmo.Add(-1);
            Guns.Add(null);
            GunCurrentClip.Add(-1);
        }

        for (int i = 0; i < ProjectileHolding; i++)
        {

        }

        for (int i = 0; i < MeleeHolding; i++)
        {

        }

        PlayerScript = gameObject.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        // Attacking Functions
        MeleeAttack();
        GunShooting();
        ProjectileShooting();

        // SwitchingWeapons
        SwitchingWeapon();
    }
    private void GunShooting()
    {
        if (CurrentGunID == -1) return;

        if (Input.GetMouseButtonDown(0) && GunCurrentClip[CurrentGunID] > 0)
        {
            switch (Guns[CurrentGunID].GunFireType)
            {
                case SO_RegularGuns.EFireType.Rapid:

                    if (CurrentGunID == 1) 
                    {
                        Instantiate(Guns[CurrentGunID].BulletPrefab);
                    }
                    break;

                case SO_RegularGuns.EFireType.Single:

                    break;
            }
        }

        UpdateClip(CurrentGunID, false);
    }

    private void ProjectileShooting()
    {
        if (CurrentProjID == -1) return;


    }

    private void MeleeAttack()
    {
        if (CurrentMeleeID == -1) return;


    }

    private void SwitchingWeapon()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            SwitchGun();
        }
    }

    private void SwitchGun()
    {
        int OriginalID = CurrentGunID;
        int TempID = CurrentGunID;
        int Loops = 0;

        while (true)
        {
            ++Loops;
            if (Loops > 1000)
            {
                break;
            }


            // If I am on the last gun, start over.
            if (TempID == GunsHolding - 1)
            {
                TempID = 0;
            }
            else
            {
                ++TempID;
            }

            if (GunAmmo[TempID] >= 0)
            {
                //I actually have that gun!!!
                CurrentGunID = TempID;
                GunHolder.transform.GetChild(OriginalID).gameObject.SetActive(false);
                GunHolder.transform.GetChild(TempID).gameObject.SetActive(true);
                UpdateClip(TempID, false);
                break;
            }

        }


    }

    internal void UpdateClip(int TempID, bool Max)
    {
        if (Max)
            GunCurrentClip[TempID] = Guns[TempID].ClipSize;
    }
}
