using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public SO_RegularGuns GunPicker;
    public int GunClipOnPickup = 32;

    private Transform MousePos;

    private void Start()
    {
        GameObject PickUpEmpty = null;
        PickUpEmpty = Instantiate(GunPicker.GunPrefab, transform.position, Quaternion.identity);
        PickUpEmpty.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeaponSelect PlayerGun = other.GetComponent<PlayerWeaponSelect>();

            gameObject.SetActive(false);

            if (PlayerGun.CurrentGunID == -1)
            {
                PlayerGun.CurrentGunID = GunPicker.GunID;
                Instantiate(GunPicker.GunPrefab, PlayerGun.GunHolder.transform.position,
                    Quaternion.Euler(GunPicker.GunPrefab.transform.forward), PlayerGun.GunHolder.transform);
                //PlayerGun.GunCurrentClip[GunPicker.GunID] = GunPicker.ClipSize;
                PlayerGun.WeaponAdded(GunPicker);

            }

            if (PlayerGun.GunAmmo[GunPicker.GunID] == -1)
            {
                PlayerGun.GunAmmo[GunPicker.GunID] = 0;
                ++PlayerGun.ActiveGuns;
                PlayerGun.Guns[GunPicker.GunID] = GunPicker;
            }

            PlayerGun.GunAmmo[GunPicker.GunID] += GunClipOnPickup;

            PlayerGun.UpdateClip(GunPicker.GunID, true);
        }
    }
}
