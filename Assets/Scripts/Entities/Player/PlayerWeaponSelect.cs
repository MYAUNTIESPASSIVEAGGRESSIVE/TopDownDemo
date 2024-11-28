using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("References")]
    public GameObject GunHolder;
    public GameObject MeleeHolder;
    public PlayerControl PlayerScript;
    public float GameMaxShootDistance;
    public LayerMask HittableLayer;

    private Weapons WeaponsScript;

    //Weapon Lists
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Weapons")]
    public List<SO_Weaponry> Weapons = new List<SO_Weaponry>();
    public List<int> WeaponAmmo = new List<int>();
    public List<int> WeaponCurrentClip = new List<int>();


    [Header("Weapon Inv Variables")]
    //Gun Inv
    public int WeaponsHolding = 0;
    public int ActiveWeapons;
    //Melee inv
    public int MeleeHolding = 0;
    public int ActiveMelees;


    [Header("Weapon ID Stats")]
    public int CurrentWeaponID = -1;
    public int CurrentMeleeID = -1;

    private bool HoldToShoot = false;
    private bool ChargingShot = false;
    private bool Overheated = false;
    private bool HoldToUse = false;
    private float CurrentTimer;
    private float CurrentHeater;
    private float CurChargeTime;

    private void Start()
    {
        WeaponsHolding = GunHolder.transform.childCount;
        MeleeHolding = MeleeHolder.transform.childCount;

        for (int i = 0; i < MeleeHolding; i++)
        {
            MeleeHolder.transform.GetChild(i).gameObject.SetActive(false);
            Melees.Add(null);
        }

        for(int i = 0; i < WeaponsHolding; i++)
        {
            GunHolder.transform.GetChild(i).gameObject.SetActive(false);
            WeaponAmmo.Add(-1);
            Weapons.Add(null);
            WeaponCurrentClip.Add(-1);
        }

        PlayerScript = gameObject.GetComponent<PlayerControl>();
        WeaponsScript = gameObject.GetComponent<Weapons>();
    }

    private void Update()
    {
        HandleGunShooting();
        HandleMeleeUse();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HandleWeaponSwitching(CurrentWeaponID, GunHolder);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            HandleWeaponSwitching(CurrentMeleeID, MeleeHolder);
        }
    }

    private void HandleGunShooting()
    {
        if (CurrentWeaponID == -1) return;

        if (Input.GetMouseButtonDown(0) && !Overheated)
        {
            if (WeaponCurrentClip[CurrentWeaponID] > 0)
            {
                switch (Weapons[CurrentWeaponID].EWeaponType)
                {
                    case SO_Weaponry.WeaponType.RegularGun:
                        GunShooting();
                        break;

                    case SO_Weaponry.WeaponType.Projectile:
                        ProjectileShooting();
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (Weapons[CurrentWeaponID].EUseType)
            {
                case SO_Weaponry.UseType.Hold:
                    HoldToShoot = false;
                    break;
                case SO_Weaponry.UseType.Charge:
                    ChargingShot = false;
                    break;
            }
        }

        if (WeaponCurrentClip[CurrentWeaponID] == 0)
        {
            HandleGunReloading(CurrentWeaponID, false);
        }

        if (WeaponCurrentClip[CurrentWeaponID] == 0)
        {
            int MaxWeaponClip = Weapons[CurrentWeaponID].ClipSize;
            int CurrentAmmo = WeaponAmmo[CurrentWeaponID];
            if (CurrentAmmo < MaxWeaponClip)
            {
                WeaponCurrentClip[CurrentWeaponID] = CurrentAmmo;
                WeaponAmmo[CurrentWeaponID] = 0;
            }
            else
            {
                WeaponCurrentClip[CurrentWeaponID] = MaxWeaponClip;
                WeaponAmmo[CurrentWeaponID] -= MaxWeaponClip;
            }
        }

        if (WeaponCurrentClip[CurrentWeaponID] == 0 && WeaponAmmo[CurrentWeaponID] == 0)
        {
            if (HoldToShoot) HoldToShoot = false;
            ChargingShot = false;
        }

        if (ChargingShot)
        {
            if (WeaponCurrentClip[CurrentWeaponID] > 0)
            {
                CurChargeTime += Time.deltaTime;
                if (CurChargeTime > Weapons[CurrentWeaponID].TimeToShoot)
                {
                    HoldToShoot = true;
                }
            }
        }

        if (HoldToShoot)
        {
            if (WeaponCurrentClip[CurrentWeaponID] > 0)
            {
                CurrentTimer += Time.deltaTime;
                if (CurrentTimer > Weapons[CurrentWeaponID].FireRate)
                {
                    --WeaponCurrentClip[CurrentWeaponID];
                    GunRayShoot();
                    CurrentTimer = 0;
                }
            }
            else
            {
                HoldToShoot = false;
            }

            if (Weapons[CurrentWeaponID].Overheatable == true)
            {
                CurrentHeater += Time.deltaTime;
                if (CurrentHeater > Weapons[CurrentWeaponID].TimeToHeat)
                {
                    Overheated = true;
                    StartCoroutine(CoolDownToShoot(Weapons[CurrentWeaponID].TimeToCool));
                    HoldToShoot = false;
                }
            }
        }

    }

    private void GunShooting()
    {
        switch (Weapons[CurrentWeaponID].EUseType)
        {
            case SO_Weaponry.UseType.Single:
                GunRayShoot();
                --WeaponCurrentClip[CurrentWeaponID];
                break;
            case SO_Weaponry.UseType.Hold:
                HoldToShoot = true;
                break;
            case SO_Weaponry.UseType.Charge:
                ChargingShot = true;
                break;
        }
    }

    private IEnumerator CoolDownToShoot(int timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        Overheated = false;
        CurrentHeater = 0;
    }
    private void ProjectileShooting()
    {
        switch (Weapons[CurrentWeaponID].EUseType)
        {
            case SO_Weaponry.UseType.Single:
                ProjShooter();
                --WeaponCurrentClip[CurrentWeaponID];
                break;
            case SO_Weaponry.UseType.Hold:
                HoldToShoot = true;
                break;
        }
    }

    private void ProjShooter()
    {
        GameObject Bullet = Instantiate(Weapons[CurrentWeaponID].WeaponBulletPrefab,
            GunHolder.transform.position, Quaternion.identity);

        Bullet.GetComponent<ProjectileBullet>().SOWeapon = Weapons[CurrentWeaponID];
    }

    private void GunRayShoot()
    {
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(GunHolder.transform.position, PlayerScript.direction, Weapons[CurrentWeaponID].MaxDistance, HittableLayer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyStats>().TakeDamage(Weapons[CurrentWeaponID].Damage, false);
            }
        }

        WeaponsScript.PlayShotAudio(Weapons[CurrentWeaponID].WeaponAudio);
    }

    private void HandleMeleeUse()
    {
        if (CurrentMeleeID == -1) return;

        if (Input.GetMouseButtonDown(0))
        {
            switch (Melees[CurrentMeleeID].EMeleeType)
            {
                case SO_Melee.MeleeUseType.Single:
                    MeleeHolder.transform.GetChild(CurrentMeleeID).GetComponent<MeleeWeapons>().MeleeUse(Melees[CurrentMeleeID]);
                    MeleeHolder.transform.GetChild(CurrentMeleeID).GetComponent<BoxCollider2D>().enabled = true;
                    break;
                case SO_Melee.MeleeUseType.Hold:
                    HoldToUse = true;
                    break;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            MeleeHolder.transform.GetChild(CurrentMeleeID).GetComponent<BoxCollider2D>().enabled = false;
            HoldToUse = false;
        }

        if (HoldToUse)
        {
            float useRate = Melees[CurrentMeleeID].UseSpeed;

            CurrentTimer += Time.deltaTime;

            if (CurrentTimer > useRate)
            {
                MeleeHolder.transform.GetChild(CurrentMeleeID).GetComponent<MeleeWeapons>().MeleeUse(Melees[CurrentMeleeID]);
                MeleeHolder.transform.GetChild(CurrentMeleeID).GetComponent<BoxCollider2D>().enabled = true;
                CurrentTimer = 0;
            }
        }
        else HoldToUse = false;
    }

    private void HandleWeaponSwitching(int WeaponID, GameObject Holder)
    {
        if (CurrentWeaponID == -1 || CurrentMeleeID == -1) return;

        UpdateInventory(WeaponID, Holder);
    }

    private void UpdateInventory(int WeaponID, GameObject Holder)
    {
        int CurrentID = WeaponID;
        int NextID = WeaponID;

        while (true)
        {
            if (NextID == WeaponsHolding - 1 || NextID == MeleeHolding - 1)
            {
                NextID = 0;
            }
            else
            {
                ++NextID;
            }

            if (WeaponAmmo[NextID] >= 0 || Melees[NextID])
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
            WeaponCurrentClip[GunID] = Weapons[GunID].ClipSize;
        }
    }

    private void UpdateUI()
    {

    }

    public void UpdateGunInv(SO_Weaponry SOGun)
    {
        
        CurrentWeaponID = SOGun.WeaponID;
        GunHolder.transform.GetChild(SOGun.WeaponID).gameObject.SetActive(true);
        WeaponCurrentClip[SOGun.WeaponID] = SOGun.ClipSize;
        
        foreach (Transform child in MeleeHolder.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void UpdateMeleeInv(SO_Melee SOMelee)
    {
        CurrentMeleeID = SOMelee.MeleeID;
        MeleeHolder.transform.GetChild(SOMelee.MeleeID).gameObject.SetActive(true);

        foreach (Transform child in GunHolder.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
