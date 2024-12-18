using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weaponry/Weapon", order = 1)]
public class SO_Weaponry : ScriptableObject
{
    [Header("Weapon Key Components")]
    public string WeaponName;
    public string WeaponDescription;
    public int Damage;
    public int WeaponID;
    public WeaponType EWeaponType;
    public UseType EUseType;

    public enum WeaponType
    {
        RegularGun,
        Projectile
    }

    public enum UseType
    {
        Single,
        Hold,
        Charge
    }

    public enum DebuffType
    {
        None,
        SlowDown,
        Poison,
        Fire
    }

    public DebuffType EDebuffType;

    [Header("Gun Variables")]
    public float FireRate;
    public float TimeToShoot;
    public float TimeToHeat;
    public int TimeToCool;
    public bool UsesFuel;

    public Vector2 ClipSizeOnPickup;


    [Header("Bullet Variables")]
    public int MaxDistance = 999;
    public int SpeedToDestroy;
    public int BulletsPerShot;
    public float AnglePerShot;
    public bool Overheatable;
    public bool ExplodeOnImpact;
    public int ClipSize;
    public int MaxReserveSize;
    public float BulletSpeed;
    public bool SticksToGun;

    [Range(1,20)]
    public float AOERange;

    [Header("Weapon Audio")]
    public AudioClip[] WeaponAudio;

    [Header("Weapon Prefab")]
    public GameObject WeaponPrefab;
    public GameObject WeaponBulletPrefab;
}
