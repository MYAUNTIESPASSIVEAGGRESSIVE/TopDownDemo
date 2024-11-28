using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class PlayerWeaponSelect : MonoBehaviour
{
    [Header("References")]
    public GameObject GunHolder;
    public GameObject MeleeHolder;
    public TMP_Text AmmoText;
    public TMP_Text WeaponName;
    public Slider FuelSlider;
    public Slider OverheatSlider;
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

        AmmoText.text = "";
        WeaponName.text = "";

        FuelSlider.gameObject.SetActive(false);
        OverheatSlider.gameObject.SetActive(false);
        OverheatSlider.value = 0;

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

        HandleWeaponSwitching();
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


        HandleGunReloading(CurrentWeaponID, false);

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
                    ProjShooter();
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
        if (Weapons[CurrentWeaponID].Overheatable)
        {

        }


        switch (Weapons[CurrentWeaponID].EUseType)
        {
            case SO_Weaponry.UseType.Single:
                ProjShooter();
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

    private void GunRayShot()
    {

    }

    private void ProjShooter()
    {
        for (int i = 0; i < Weapons[CurrentWeaponID].BulletsPerShot; ++i)
        {
            GameObject Bullet = Instantiate(Weapons[CurrentWeaponID].WeaponBulletPrefab,
                GunHolder.transform.position, Quaternion.identity);

            Bullet.GetComponent<ProjectileBullet>().SOWeapon = Weapons[CurrentWeaponID];
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

    private void HandleWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && CurrentWeaponID != -1 && ActiveWeapons > 0)
        {
            if(MeleeHolding > 0 && MeleeHolder.transform.GetChild(CurrentMeleeID).gameObject.activeSelf)
            {
                MeleeHolder.transform.GetChild(CurrentMeleeID).gameObject.SetActive(false);
                UpdateInventory(GunHolder);
            }
            
            UpdateInventory(GunHolder);
        }

        if (Input.GetKeyDown(KeyCode.M) && CurrentMeleeID != -1 && ActiveMelees > 0)
        {
            if (GunHolder.transform.GetChild(CurrentWeaponID).gameObject.activeSelf)
            {
                GunHolder.transform.GetChild(CurrentWeaponID).gameObject.SetActive(false);
                MeleeHolder.transform.GetChild(CurrentMeleeID).gameObject.SetActive(true);
            }
            else MeleeHolder.transform.GetChild(CurrentMeleeID).gameObject.SetActive(true);
        }
    }

    private void UpdateInventory(GameObject Holder)
    {
        int CurrentID = CurrentWeaponID;
        int NextID = CurrentWeaponID;

        while (true)
        {
            if (NextID == WeaponsHolding - 1)
            {
                NextID = 0;
                
            }
            else
            {
                ++NextID;
            }

            if (WeaponAmmo[NextID] >= 0)
            {
                //I actually have that gun!!!
                CurrentWeaponID = NextID;
                Holder.transform.GetChild(CurrentID).gameObject.SetActive(false);
                Holder.transform.GetChild(NextID).gameObject.SetActive(true);
                HandleGunReloading(NextID,false);
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

        UpdateUI(GunID);
    }

    private void UpdateUI(int ID)
    {
        if (Weapons[CurrentWeaponID].UsesFuel)
        {
            WeaponName.text = Weapons[ID].WeaponName;
            FuelSlider.gameObject.SetActive(true);

            FuelSlider.value = WeaponCurrentClip[CurrentWeaponID];
        }
        else
        {
            AmmoText.text = WeaponCurrentClip[ID] + "/" + WeaponAmmo[ID];
            WeaponName.text = Weapons[ID].WeaponName;
            FuelSlider.gameObject.SetActive(false);
        }

        if (Weapons[CurrentWeaponID].Overheatable)
        {
            OverheatSlider.maxValue = Weapons[CurrentWeaponID].TimeToHeat;
            OverheatSlider.gameObject.SetActive(true);
            
        }
        else OverheatSlider.gameObject.SetActive(false);
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
