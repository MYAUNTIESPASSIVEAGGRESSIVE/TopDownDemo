using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public SO_RegularGuns GunPicker;
    public int GunClipOnPickup = 32;

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
            gameObject.SetActive(false);

            PlayerWeaponSelect PlayerGun = other.GetComponent<PlayerWeaponSelect>();

            if (PlayerGun.CurrentGunID == -1)
            {
                PlayerGun.CurrentGunID = GunPicker.GunID;
                PlayerGun.GunHolder.transform.GetChild(GunPicker.GunID).gameObject.SetActive(true);
                PlayerGun.GunCurrentClip[GunPicker.GunID] = GunPicker.ClipSize;

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
