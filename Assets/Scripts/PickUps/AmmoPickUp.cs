using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerWeaponSelect WeaponSelect = collision.transform.GetComponent<PlayerWeaponSelect>();

            if(WeaponSelect.CurrentWeaponID > 0)
            {
                WeaponSelect.WeaponAmmo[WeaponSelect.CurrentWeaponID] +=
                    (int)Random.Range(WeaponSelect.Weapons[WeaponSelect.CurrentWeaponID].ClipSizeOnPickup.x,
                    WeaponSelect.Weapons[WeaponSelect.CurrentWeaponID].ClipSizeOnPickup.y);
            }
        }
    }
}
