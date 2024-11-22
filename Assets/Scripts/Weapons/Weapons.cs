using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerWeaponSelect WeaponSelectScript;

    //private bool HoldingGun = false;
    //private bool HoldingProj = false;
    //private bool HoldingMelee = false;

    private void Start()
    {
        WeaponSelectScript = transform.GetComponent<PlayerWeaponSelect>();
    }

    public virtual void Update()
    {

    }

    public virtual void UseWeapon(GameObject Bullet, Transform Weapon, int BulletCount, float AnglePerShot, int Speed, float ShootDistance)
    {
        for (int i = 0; i < BulletCount; i++)
        {
            Instantiate(Bullet, Weapon);
            Bullet.AddComponent<Rigidbody2D>();
            Bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
            Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * Speed * Time.deltaTime;


        }
    }

    public virtual void PlayShotAudio(AudioClip[] ShootSound, AudioSource audioSource)
    {
        int MaxClipAmmount = ShootSound.Length;

        audioSource.PlayOneShot(ShootSound[Random.Range(0, MaxClipAmmount)]);
    }

    public virtual void PlayReloadAudio(AudioClip ReloadSound, AudioSource audioSource)
    {
        audioSource.PlayOneShot(ReloadSound);
    }
}
