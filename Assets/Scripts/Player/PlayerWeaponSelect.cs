using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{
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
        for (int i = 0; i < GunsHolding; i++)
        {

        }

        for (int i = 0; i < ProjectileHolding; i++)
        {

        }

        for (int i = 0; i < MeleeHolding; i++)
        {

        }
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

        }
    }
}
