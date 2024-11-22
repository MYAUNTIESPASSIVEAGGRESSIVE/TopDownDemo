using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regular Gun", menuName = "Weaponry/Regular Gun", order = 1)]
public class SO_RegularGuns : ScriptableObject
{
    public string GunName;
    public string GunDescription;

    public enum ShootingType
    {
        Single,
        Hold,
        Charge,

    }
    public ShootingType GShootType;

    // Any Gun Specifics
    public int FireRate;
    public int ClipSize;
    public int BulletsShotPerClick;
    public int ShotSpeed;
    public float ShotDistance;

    // For Charge/Overheat Mechanic
    public bool Overheatable;
    public int TimeToShoot;
    public float MaxTimeToHeat;
    public int ShootCooldown;


    public int GunID;
    public int Damage;

    public float AnglePerShot;

    public AudioClip[] ShootAudio;
    public AudioSource ShootSource;

    public GameObject BulletPrefab;
    public GameObject GunPrefab;
}
