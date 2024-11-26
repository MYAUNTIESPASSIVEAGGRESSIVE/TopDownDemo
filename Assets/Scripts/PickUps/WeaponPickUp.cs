using UnityEngine;
using UnityEngine.UIElements;

public class WeaponPickUp : MonoBehaviour
{
    public SO_RegularGuns GunPicker;

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

            //GunPickup Logic
            if (PlayerGun.CurrentGunID == -1)
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

            PlayerGun.HandleGunReloading(GunPicker.GunID, true);

        }
    }
}
