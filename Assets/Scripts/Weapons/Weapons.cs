using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerWeaponSelect WeaponSelectScript;

    private bool HoldingGun = false;
    private bool HoldingProj = false;
    private bool HoldingMelee = false;

    private void Start()
    {
        WeaponSelectScript = transform.GetComponent<PlayerWeaponSelect>();
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
