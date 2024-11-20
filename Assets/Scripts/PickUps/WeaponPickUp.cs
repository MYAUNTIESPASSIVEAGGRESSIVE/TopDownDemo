using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public SO_RegularGuns GunPicker;
    public int GunClipOnPickup = 32;
    public int ReserveOnPickup = 100;

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
        }
    }
}
