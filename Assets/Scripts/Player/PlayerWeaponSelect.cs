using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Regular Guns")]
    public List<SO_RegularGuns> Guns = new List<SO_RegularGuns> ();

    [Header("Projectile Weapons")]
    public List<SO_Projectile> Projectiles = new List<SO_Projectile>();

    private void Update()
    {
        // Attacking Functions
        MeleeAttack();
        GunShooting();
        ProjectileShooting();

        // SwitchingWeapons
        SwitchingWeapon();
    }

    private void ProjectileShooting()
    {
        
    }

    private void GunShooting()
    {
        
    }

    private void MeleeAttack()
    {

    }
    private void SwitchingWeapon()
    {

    }
}
