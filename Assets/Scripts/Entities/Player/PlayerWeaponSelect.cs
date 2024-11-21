using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("References")]
    public GameObject WeaponHolder;
    public PlayerControl PlayerScript;

    //Weapon Lists
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Regular Guns")]
    public List<SO_RegularGuns> Guns = new List<SO_RegularGuns>();
    private List<int> GunAmmo = new List<int>();
    private List<int> GunClipCurrent = new List<int>();

    [Header("Projectile Weapons")]
    public List<SO_Projectile> Projectiles = new List<SO_Projectile>();
    private List<int> ProjAmmo = new List<int>();
    private List<int> ProjCurrent = new List<int>();


    [Header("Weapon Inv Variables")]
    //Gun Inv
    public int GunsHolding = 0;
    //Projectile Inv
    public int ProjectileHolding = 0;
    //Melee inv
    public int MeleeHolding = 0;


    [Header("Weapon ID Stats")]
    public int CurrentGunID = -1;
    public int CurrentMeleeID = -1;
    public int CurrentProjID = -1;

    private GameObject WeaponHeld;

    private string WeaponName;

    private bool HoldToShoot;
    private bool Charging;
    private float CurrentTimer;
    private float CurrentHeater;
    private int CurChargeTime;



    private void Start()
    {
        PlayerScript = gameObject.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        HandleGunShooting();



        HandleWeaponSwitching();
        HandleWeaponReloading();
    }

    private void HandleGunShooting()
    {
        if (Input.GetMouseButtonDown(0) && GunClipCurrent[CurrentGunID] > 0)
        {
            switch (Guns[CurrentGunID].GShootType)
            {
                case SO_RegularGuns.ShootingType.Single:
                    WeaponHeld.GetComponent<RegularGuns>().ShootingGun(Guns[CurrentGunID]);
                    break;

                case SO_RegularGuns.ShootingType.Hold:
                    HoldToShoot = true;
                    break;
                case SO_RegularGuns.ShootingType.Charge:
                    int ChargeTime = Guns[CurrentGunID].TimeToShoot;
                    CurrentTimer += Time.deltaTime;
                    if (CurChargeTime > ChargeTime)
                    {
                        HoldToShoot = true;
                    }
                    break;
            }
            //WeaponHeld.GetComponent<ProjectileGuns>().ShootingGun(Guns[CurrentProjID]);
            //WeaponHeld.GetComponent<MeleeWeapons>().ShootingGun(Guns[CurrentMeleeID]);
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (Guns[CurrentGunID].GShootType)
            {
                case SO_RegularGuns.ShootingType.Hold:
                    HoldToShoot = false;
                    break;
            }
        }

        if (HoldToShoot) 
        {
            if (GunClipCurrent[CurrentGunID] > 0)
            {
                
                float FireRate = Guns[CurrentGunID].FireRate;
                bool Heated = Guns[CurrentGunID].Overheatable;
                float HeatTimer = Guns[CurrentGunID].MaxTimeToHeat;

                CurrentTimer += Time.deltaTime;

                if (CurrentTimer > FireRate)
                {
                    WeaponHeld.GetComponent<RegularGuns>().ShootingGun(Guns[CurrentGunID]);
                    --GunClipCurrent[CurrentGunID];
                    CurrentTimer = 0;
                }

                if (CurrentTimer > FireRate && Heated)
                {
                    HeatTimer += Time.deltaTime;

                    if (CurrentHeater > HeatTimer)
                    {
                        HoldToShoot = false;
                        CurrentHeater = 0;
                    }
                }
            }
            else HoldToShoot = false;
        }
    }

    public void WeaponAdded(SO_RegularGuns GunType)
    {
        GunsHolding = WeaponHolder.transform.childCount;
        ProjectileHolding = WeaponHolder.transform.childCount;
        MeleeHolding = WeaponHolder.transform.childCount;

        for (int i = 0; i < GunsHolding; i++)
        {
            Guns.Add(GunType);
        }

        for (int i = 0; i < ProjectileHolding; i++)
        {

        }

        for (int i = 0; i < MeleeHolding; i++)
        {

        }
    }

    private void HandleWeaponSwitching()
    {

    }

    private void HandleWeaponReloading()
    {

    }
}
