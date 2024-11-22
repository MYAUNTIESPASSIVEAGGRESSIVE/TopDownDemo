using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGuns : Weapons
{
    public GameObject Muzzle;

    public void ProjectileThrow(SO_Projectile SOProj, float ShootDistanceMult)
    {
        base.UseWeapon(SOProj.ProjBulletPrefab, Muzzle.transform, SOProj.ProjPerShot, 0, SOProj.ShotSpeed, ShootDistanceMult);

        base.PlayShotAudio(SOProj.ProjAudio, SOProj.ProjSource);
    }


    public void ProjectileShoot(SO_Projectile SOProj, float ShootDistanceMult)
    {
        base.UseWeapon(SOProj.ProjBulletPrefab, Muzzle.transform, SOProj.ProjPerShot, 0, SOProj.ShotSpeed, ShootDistanceMult);

        base.PlayShotAudio(SOProj.ProjAudio, SOProj.ProjSource);
    }
}
