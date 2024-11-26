using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerWeaponSelect WeaponSelectScript;
    private AudioSource audioSource;

    //private bool HoldingGun = false;
    //private bool HoldingProj = false;
    //private bool HoldingMelee = false;

    private void Start()
    {
        WeaponSelectScript = transform.GetComponent<PlayerWeaponSelect>();

        audioSource = GetComponentInChildren<AudioSource>();
    }

    public virtual void Update()
    {

    }

    public virtual void UseWeapon(GameObject Bullet, Transform Weapon, int BulletCount, 
        float AnglePerShot, int Speed, float ShootDistance, 
        bool NonMoving, int Damage)
    {
        for (int i = 0; i < BulletCount; i++)
        {
            Instantiate(Bullet, Weapon.position, Quaternion.identity);

            Bullet.GetComponent<BulletScript>().NonMoving = NonMoving;
            Bullet.GetComponent<BulletScript>().BulletDamage = Damage;
            Bullet.GetComponent<BulletScript>().BulletSpeed = Speed;
        }
    }

    public virtual void PlayShotAudio(AudioClip[] ShootSound)
    {
        int MaxClipAmmount = ShootSound.Length;

        audioSource.PlayOneShot(ShootSound[Random.Range(0, MaxClipAmmount)]);
    }

    public virtual void PlayReloadAudio(AudioClip ReloadSound)
    {
        audioSource.PlayOneShot(ReloadSound);
    }
}
