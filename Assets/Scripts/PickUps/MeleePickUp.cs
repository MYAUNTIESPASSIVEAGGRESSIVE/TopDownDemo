using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePickUp : MonoBehaviour
{
    public SO_Melee MeleePicker;

    private void Start()
    {
        GameObject PickUpEmpty = null;

        PickUpEmpty = Instantiate(MeleePicker.MeleePrefab, transform.position, Quaternion.identity);
        PickUpEmpty.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWeaponSelect PlayerGun = collision.GetComponent<PlayerWeaponSelect>();

            gameObject.SetActive(false);

            if (PlayerGun.CurrentMeleeID == -1)
            {
                PlayerGun.UpdateMeleeInv(MeleePicker);
                ++PlayerGun.ActiveProjectiles;
                PlayerGun.Melees[MeleePicker.MeleeID] = MeleePicker;
            }
        }
    }
}
