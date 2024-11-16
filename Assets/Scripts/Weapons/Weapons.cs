using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerWeaponSelect WeaponSelectScript;

    private void Start()
    {
        WeaponSelectScript = transform.GetComponent<PlayerWeaponSelect>();
    }


    public virtual void ShootingGun(SO_RegularGuns SOGun)
    {
        
    }

    public virtual void ShootingProj(SO_Projectile SOProj)
    {

    }

    public virtual void MeleeAttacking(SO_Melee SOMelee)
    {

    }

    public virtual void SpawnBullets(GameObject Bullet, Transform Weapon, int BulletCount)
    {
        for (int i = 0; i < BulletCount; i++)
        {
            Instantiate(Bullet, Weapon);
        }
    }

    public virtual void PlayShotAudio(AudioClip[] ShootSound, AudioSource audioSource)
    {
        //audioSource.PlayOneShot();
    }

    public virtual void PlayReloadAudio(AudioClip ReloadSound, AudioSource audioSource)
    {
        //audioSource.PlayOneShot(ShootSound);
    }
}
