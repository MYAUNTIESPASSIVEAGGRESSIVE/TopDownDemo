using UnityEngine;
using UnityEngine.UIElements;

public class WeaponPickUp : MonoBehaviour
{
    public SO_RegularGuns GunPicker;
    public SO_Projectile ProjPicker;
    public SO_Melee MeleePicker;

    public Transform Holder;

    public bool MeleePickup;
    public bool ProjPickup;
    public bool GunPickup;

    private int ChosenWeaponNumber;
    private GameObject ChosenWeapon;

    private void Start()
    {
        GameObject PickUpEmpty = null;

        if (MeleePickup)
        {
            ChosenWeapon = MeleePicker.MeleePrefab;
        }

        if (ProjPickup)
        {
            ChosenWeapon = ProjPicker.ProjPrefab;
        }

        if (GunPickup)
        {
            ChosenWeapon = GunPicker.GunPrefab;
        }

        PickUpEmpty = Instantiate(ChosenWeapon, transform.position, Quaternion.identity);
        PickUpEmpty.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponSelect PlayerGun = other.GetComponent<PlayerWeaponSelect>();

            gameObject.SetActive(false);

            //GunPickup Logic
            if (PlayerGun.CurrentGunID == -1 && GunPickup)
            {
                PlayerGun.UpdateGunInv(GunPicker);
            }
            if (PlayerGun.GunAmmo[GunPicker.GunID] == -1)
            {
                PlayerGun.GunAmmo[GunPicker.GunID] = 0;
                ++PlayerGun.ActiveGuns;
                PlayerGun.Guns[GunPicker.GunID] = GunPicker;
            }
            PlayerGun.GunAmmo[GunPicker.GunID] +=
            (int)Random.Range(GunPicker.ClipSizeOnPickup.x,
                GunPicker.ClipSizeOnPickup.y);

            //Projectile Pickup Logic
            if (PlayerGun.CurrentProjID == -1 && ProjPickup)
            {
                PlayerGun.UpdateProjInv(ProjPicker);
            }

            if (PlayerGun.ProjAmmo[ProjPicker.ProjID] == -1)
            {
                PlayerGun.ProjAmmo[ProjPicker.ProjID] = 0;
                ++PlayerGun.ActiveProjectiles;
                PlayerGun.Projectiles[ProjPicker.ProjID] = ProjPicker;
            }
            PlayerGun.ProjAmmo[ProjPicker.ProjID] +=
            (int)Random.Range(ProjPicker.ClipSizeOnPickup.x,
                ProjPicker.ClipSizeOnPickup.y);

            //Melee Pickup Logic
            if (PlayerGun.CurrentMeleeID == -1 && MeleePickup)
            {
                PlayerGun.UpdateMeleeInv(MeleePicker);
                ++PlayerGun.ActiveProjectiles;
                PlayerGun.Melees[MeleePicker.MeleeID] = MeleePicker;
            }

            //Update Ammo
            if(ProjPickup)
            {
                PlayerGun.HandleGunReloading(ProjPicker.ProjID, true);
            }

            if (GunPickup)
            {
                PlayerGun.HandleGunReloading(GunPicker.GunID, true);
            }

        }
    }
}
