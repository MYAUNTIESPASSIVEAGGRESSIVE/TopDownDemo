using UnityEngine;

public class RegularGuns : Weapons
{
    public GameObject Muzzle;

    public void ShootingGun(SO_RegularGuns SOGun)
    {
        base.UseWeapon(SOGun.BulletPrefab, Muzzle.transform, 
            SOGun.BulletsShotPerClick, SOGun.AnglePerShot, 
            SOGun.ShotSpeed, SOGun.ShotDistance, SOGun.NonMoving, SOGun.Damage);

        //base.PlayShotAudio(SOGun.ShootAudio);
    }
}
