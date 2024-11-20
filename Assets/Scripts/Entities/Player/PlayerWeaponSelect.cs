using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelect : MonoBehaviour
{

    //fix and redo - maybe shrink to a single function and or SO and other stuff

    [Header("References")]
    public GameObject WeaponHolder;
    public PlayerControl PlayerScript;

    //Weapon Lists
    [Header("Melee Weapons")]
    public List<SO_Melee> Melees = new List<SO_Melee>();

    [Header("Regular Guns")]
    public List<SO_RegularGuns> Guns = new List<SO_RegularGuns> ();

    [Header("Projectile Weapons")]
    public List<SO_Projectile> Projectiles = new List<SO_Projectile>();

    [Header("Weapon Inv Variables")]
    //Gun Inv
    public int GunsHolding = 0;
    //Projectile Inv
    public int ProjectileHolding = 0;
    //Melee inv
    public int MeleeHolding = 0;


    [Header("Weapon ID Stats")]
    public int CurrentGunID = -1;
    public int CurrentMeleeID = -1;
    public int CurrentProjID = -1;

    [Header("Other Gun Variables")]
    public int ActiveGuns;

    private void Start()
    {
        PlayerScript = gameObject.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        HandleWeaponsShooting();
    }

    private void HandleWeaponsShooting()
    {
        if (Input.GetMouseButtonDown(0)) 
        {

        }

    }

    public void WeaponAdded(SO_RegularGuns GunType)
    {
        GunsHolding = WeaponHolder.transform.childCount;
        ProjectileHolding = WeaponHolder.transform.childCount;
        MeleeHolding = WeaponHolder.transform.childCount;

        for (int i = 0; i < GunsHolding; i++)
        {
            Guns.Add(GunType);
        }

        for (int i = 0; i < ProjectileHolding; i++)
        {

        }

        for (int i = 0; i < MeleeHolding; i++)
        {

        }
    }
}
