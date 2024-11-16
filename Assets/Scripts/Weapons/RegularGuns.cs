using UnityEngine;

public class RegularGuns : Weapons
{
    public GameObject Muzzle;

    public override void ShootingGun(SO_RegularGuns SOGun)
    {
        SpawnBullets(SOGun.BulletPrefab, Muzzle.transform, SOGun.BulletsShotPerClick);

        PlayShotAudio(SOGun.ShootAudio, SOGun.ShootSource);
    }
}
