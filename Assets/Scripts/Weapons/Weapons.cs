using UnityEngine;

public class Weapons : MonoBehaviour
{
    private PlayerWeaponSelect WeaponSelectScript;
    protected AudioSource audioSource;

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
