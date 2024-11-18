using UnityEngine;

public class RegularGuns : Weapons
{
    public GameObject Muzzle;

    public override void ShootingGun(SO_RegularGuns SOGun)
    {
        base.SpawnBullets(SOGun.BulletPrefab, Muzzle.transform, SOGun.BulletsShotPerClick);

        base.PlayShotAudio(SOGun.ShootAudio, SOGun.ShootSource);
    }
}
