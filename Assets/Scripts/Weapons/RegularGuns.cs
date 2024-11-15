
public class RegularGuns : Weapons
{
    public override void ShootingGun(SO_RegularGuns SOGun)
    {
        SpawnBullets(SOGun.BulletPrefab, transform, SOGun.BulletsShotPerClick);

        PlayShotAudio(SOGun.ShootAudio, SOGun.ShootSource);
    }
}
