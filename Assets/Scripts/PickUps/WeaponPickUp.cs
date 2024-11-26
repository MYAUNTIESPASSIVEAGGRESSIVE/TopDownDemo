using UnityEngine;
using UnityEngine.UIElements;

public class WeaponPickUp : MonoBehaviour
{
    public SO_Weaponry GunPicker;
    
    private void Start()
    {
        GameObject PickUpEmpty = null;

        PickUpEmpty = Instantiate(GunPicker.WeaponPrefab, transform.position, Quaternion.identity);
        PickUpEmpty.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponSelect PlayerGun = other.GetComponent<PlayerWeaponSelect>();

            gameObject.SetActive(false);

            //GunPickup Logic
            if (PlayerGun.CurrentWeaponID == -1)
            {
                PlayerGun.UpdateGunInv(GunPicker);
            }
            if (PlayerGun.WeaponAmmo[GunPicker.WeaponID] == -1)
            {
                PlayerGun.WeaponAmmo[GunPicker.WeaponID] = 0;
                ++PlayerGun.ActiveWeapons;
                PlayerGun.Weapons[GunPicker.WeaponID] = GunPicker;
            }
            PlayerGun.WeaponAmmo[GunPicker.WeaponID] +=
            (int)Random.Range(GunPicker.ClipSizeOnPickup.x,
                GunPicker.ClipSizeOnPickup.y);

            PlayerGun.HandleGunReloading(GunPicker.WeaponID, true);

        }
    }
    
}
