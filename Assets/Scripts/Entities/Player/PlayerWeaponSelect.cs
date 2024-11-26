using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("References")]
    public GameObject GunHolder;
    public GameObject ProjHolder;
    public GameObject MeleeHolder;
    public PlayerControl PlayerScript;
    public float GameMaxShootDistance;

    //Weapon Lists
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Regular Guns")]
    public List<SO_RegularGuns> Guns = new List<SO_RegularGuns>();
    public List<int> GunAmmo = new List<int>();
    public List<int> GunClipCurrent = new List<int>();

    [Header("Projectile Weapons")]
    public List<SO_Projectile> Projectiles = new List<SO_Projectile>();
    public List<int> ProjAmmo = new List<int>();
    public List<int> ProjClipCurrent = new List<int>();


    [Header("Weapon Inv Variables")]
    //Gun Inv
    public int GunsHolding = 0;
    public int ActiveGuns;
    //Projectile Inv
    public int ProjectileHolding = 0;
    public int ActiveProjectiles;
    //Melee inv
    public int MeleeHolding = 0;
    public int ActiveMelees;


    [Header("Weapon ID Stats")]
    public int CurrentGunID = -1;
    public int CurrentMeleeID = -1;
    public int CurrentProjID = -1;

    private bool HoldToShoot;
    private bool ChargingShot;
    private float CurrentTimer;
    private float CurrentHeater;
    private int CurChargeTime;
    private bool HoldToUse;

    private void Start()
    {
        GunsHolding = GunHolder.transform.childCount;
        MeleeHolding = MeleeHolder.transform.childCount;
        ProjectileHolding = ProjHolder.transform.childCount;


        for (int i = 0; i < GunsHolding; i++)
        {
            GunHolder.transform.GetChild(i).gameObject.SetActive(false);
            GunAmmo.Add(-1);
            Guns.Add(null);
            GunClipCurrent.Add(-1);
        }

        for (int i = 0; i < GunsHolding; i++)
        {
            GunHolder.transform.GetChild(i).gameObject.SetActive(false);
            GunAmmo.Add(-1);
            Guns.Add(null);
            GunClipCurrent.Add(-1);
        }

        for (int i = 0; i < GunsHolding; i++)
        {
            GunHolder.transform.GetChild(i).gameObject.SetActive(false);
            GunAmmo.Add(-1);
            Guns.Add(null);
            GunClipCurrent.Add(-1);
        }

        PlayerScript = gameObject.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        HandleGunShooting();
        HandleMeleeUse();
        HandleProjShooting();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HandleWeaponSwitching(CurrentGunID,GunHolder);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            HandleWeaponSwitching(CurrentMeleeID, MeleeHolder);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            
            HandleWeaponSwitching(CurrentProjID, ProjHolder);
        }
    }

    private void HandleGunShooting()
    {
        if (CurrentGunID == -1) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (GunClipCurrent[CurrentGunID] > 0)
            {
                Debug.Log("Shooting");
                switch (Guns[CurrentGunID].GShootType)
                {
                    case SO_RegularGuns.ShootingType.Single:
                        GunHolder.GetComponentInChildren<RegularGuns>().ShootingGun(Guns[CurrentGunID]);
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
            }
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
            float FireRate = Guns[CurrentGunID].FireRate;
            bool Heated = Guns[CurrentGunID].Overheatable;
            float HeatTimer = Guns[CurrentGunID].MaxTimeToHeat;

            if (GunClipCurrent[CurrentGunID] > 0)
            {
                CurrentTimer += Time.deltaTime;
                if (CurrentTimer > FireRate)
                {
                    Debug.Log("SHoot BooletFFs");
                    --GunClipCurrent[CurrentGunID];
                    GunHolder.GetComponentInChildren<RegularGuns>().ShootingGun(Guns[CurrentGunID]);
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

        if (GunClipCurrent[CurrentGunID] <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            HandleGunReloading(CurrentGunID, false);
        }

    }

    private void HandleMeleeUse()
    {
        if (CurrentMeleeID == -1) return;

        if (Input.GetMouseButtonDown(0))
        {
            switch (Melees[CurrentMeleeID].EMeleeType)
            {
                case SO_Melee.MeleeUseType.Single:
                    MeleeHolder.GetComponentInChildren<MeleeWeapons>().MeleeUse(Melees[CurrentMeleeID], Melees[CurrentMeleeID].ShootOut);
                    break;
                case SO_Melee.MeleeUseType.Hold:
                    HoldToUse = true;
                    break;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            HoldToUse = false;
        }

        if (HoldToUse)
        {
            float useRate = Melees[CurrentMeleeID].UseSpeed;

            CurrentTimer += Time.deltaTime;

            if (CurrentTimer > useRate)
            {
                MeleeHolder.GetComponentInChildren<MeleeWeapons>().MeleeUse(Melees[CurrentMeleeID], Melees[CurrentMeleeID].ShootOut);

                CurrentTimer = 0;
            }
        }
        else HoldToUse = false;
    }

    private void HandleProjShooting()
    {
        if (CurrentMeleeID == -1 || ProjAmmo[CurrentProjID] <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (ProjClipCurrent[CurrentProjID] > 0)
            {
                switch (Projectiles[CurrentProjID].EProjType)
                {
                    case SO_Projectile.ProjType.Shootable:
                        ProjectileShooting();
                        break;

                    case SO_Projectile.ProjType.Throwable:
                        ProjectileThrowing();
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChargingShot = false;
        }

        if (ProjClipCurrent[CurrentProjID] <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            HandleProjReloading(CurrentProjID, false);
        }
    }

    private void ProjectileShooting()
    {
        switch (Projectiles[CurrentProjID].EProjUse)
        {
            case SO_Projectile.ProjUseType.Single:
                ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileShoot(Projectiles[CurrentProjID], Projectiles[CurrentProjID].ShootDistance);
                --ProjClipCurrent[CurrentProjID];
                break;
            case SO_Projectile.ProjUseType.Charge:
                ChargingShot = true;
                break;
        }

        if (ChargingShot)
        {
            int ChargeTime = Projectiles[CurrentProjID].TimeToUse;
            float ShotDist = Projectiles[CurrentProjID].ShootDistance;
            float CurrentShotDist;

            CurrentTimer += Time.deltaTime;
            if (CurChargeTime > ChargeTime)
            {
                CurrentShotDist = ShotDist + 2f;
                ChargingShot = false;

                if (Input.GetMouseButtonUp(0))
                {
                    float MaxShotDistance = CurrentShotDist;

                    ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileShoot(Projectiles[CurrentProjID], MaxShotDistance);
                    --ProjClipCurrent[CurrentProjID];

                    ChargingShot = false;
                }
                else if (CurrentShotDist >= GameMaxShootDistance)
                {
                    float MaxShotDistance = CurrentShotDist;

                    ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileShoot(Projectiles[CurrentProjID], MaxShotDistance);
                    --ProjClipCurrent[CurrentProjID];

                    ChargingShot = false;
                }
            }
        }
        else ChargingShot = false;
    }

    private void ProjectileThrowing()
    {
        switch (Projectiles[CurrentProjID].EProjUse)
        {
            case SO_Projectile.ProjUseType.Single:
                ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileThrow(Projectiles[CurrentProjID], Projectiles[CurrentProjID].ShootDistance);
                --ProjClipCurrent[CurrentProjID];
                break;
            case SO_Projectile.ProjUseType.Charge:
                ChargingShot = true;
                break;
        }

        if (ChargingShot)
        {
            int ChargeTime = Projectiles[CurrentProjID].TimeToUse;
            float ShotDist = Projectiles[CurrentProjID].ShootDistance;
            float CurrentShotDist;

            CurrentTimer += Time.deltaTime;
            if (CurChargeTime > ChargeTime)
            {
                CurrentShotDist = ShotDist + 2f;
                ChargingShot = false;

                if (Input.GetMouseButtonUp(0))
                {
                    float MaxShotDistance = CurrentShotDist;

                    ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileThrow(Projectiles[CurrentProjID], MaxShotDistance);
                    --ProjClipCurrent[CurrentProjID];
                    ChargingShot = false;
                }
                else if (CurrentShotDist >= GameMaxShootDistance)
                {
                    float MaxShotDistance = CurrentShotDist;

                    ProjHolder.GetComponentInChildren<ProjectileGuns>().ProjectileThrow(Projectiles[CurrentProjID], MaxShotDistance);
                    --ProjClipCurrent[CurrentProjID];
                    ChargingShot = false;
                }
            }
        }
        else ChargingShot = false;
    }

    private void HandleWeaponSwitching(int WeaponID, GameObject Holder)
    {
        if (CurrentGunID == -1 || CurrentMeleeID == -1 || CurrentProjID == -1) return;

        UpdateInventory(WeaponID, Holder);
    }

    private void UpdateInventory(int WeaponID, GameObject Holder)
    {
        int CurrentID = WeaponID;
        int NextID = WeaponID;

        while (true)
        {
            if (NextID == GunsHolding - 1 || NextID == ProjectileHolding - 1 || NextID == MeleeHolding - 1)
            {
                NextID = 0;
            }
            else
            {
                ++NextID;
            }

            if (GunAmmo[NextID] >= 0 || ProjAmmo[NextID] >= 0 || Melees[NextID])
            {
                //I actually have that gun!!!
                WeaponID = NextID;
                Holder.transform.GetChild(CurrentID).gameObject.SetActive(false);
                Holder.transform.GetChild(NextID).gameObject.SetActive(true);

                break;
            }

        }
    }

    internal void HandleGunReloading(int WeaponID, bool AmmoFull)
    {
        int GunID = WeaponID;

        if(AmmoFull)
        {
            GunClipCurrent[GunID] = Guns[GunID].ClipSize;
            GunAmmo[GunID] = GunAmmo[GunID] - Guns[GunID].ClipSize;
        }
    }

    internal void HandleProjReloading(int WeaponID, bool AmmoFull)
    {
        if (AmmoFull)
        {
            ProjClipCurrent[WeaponID] = Projectiles[WeaponID].ClipSize;
            ProjAmmo[WeaponID] = ProjAmmo[WeaponID] - Projectiles[WeaponID].ClipSize;
        }
    }

    public void UpdateGunInv(SO_RegularGuns SOGun)
    {
        CurrentGunID = SOGun.GunID;
        GunHolder.transform.GetChild(SOGun.GunID).gameObject.SetActive(true);
        GunClipCurrent[SOGun.GunID] = SOGun.ClipSize;
    }

    public void UpdateProjInv(SO_Projectile SOProj)
    {
        CurrentProjID = SOProj.ProjID;
        ProjHolder.transform.GetChild(SOProj.ProjID).gameObject.SetActive(true);
        ProjClipCurrent[SOProj.ProjID] = SOProj.ClipSize;
    }

    public void UpdateMeleeInv(SO_Melee SOMelee)
    {
        CurrentMeleeID = SOMelee.MeleeID;
        MeleeHolder.transform.GetChild(SOMelee.MeleeID).gameObject.SetActive(true);
    }
}
