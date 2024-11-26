using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjPickUp : MonoBehaviour
{
    public SO_Projectile ProjPicker;

    private void Start()
    {
        GameObject PickUpEmpty = null;

        PickUpEmpty = Instantiate(ProjPicker.ProjPrefab, transform.position, Quaternion.identity);
        PickUpEmpty.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWeaponSelect PlayerGun = collision.GetComponent<PlayerWeaponSelect>();

            gameObject.SetActive(false);

            if (PlayerGun.CurrentProjID == -1)
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

            PlayerGun.HandleProjReloading(ProjPicker.ProjID, true);
        }
    }
}
