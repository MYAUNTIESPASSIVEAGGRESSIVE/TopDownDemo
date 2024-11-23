using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regular Gun", menuName = "Weaponry/Regular Gun", order = 1)]
public class SO_RegularGuns : ScriptableObject
{
    [Header("Gun Key Compontents")]
    public string GunName;
    public string GunDescription;
    public int GunID;
    public int Damage;

    public enum ShootingType
    {
        Single,
        Hold,
        Charge,

    }
    [Header("ShootType")]
    public ShootingType GShootType;

    [Header("GunSpecs")]
    public int FireRate;
    public int ClipSize;
    public int ReserveSize;
    public Vector2 ClipSizeOnPickup;
    public int BulletsShotPerClick;
    public int ShotSpeed;
    public float ShotDistance;
    public float AnglePerShot;

    [Header("For Heat/Charge")]
    public bool Overheatable;
    public int TimeToShoot;
    public float MaxTimeToHeat;
    public int ShootCooldown;

    [Header("Audio")]
    public AudioClip[] ShootAudio;
    public AudioSource ShootSource;

    [Header("Prefabs")]
    public GameObject BulletPrefab;
    public GameObject GunPrefab;
}
