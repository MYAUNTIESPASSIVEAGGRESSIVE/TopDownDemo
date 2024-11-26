using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Weaponry/Projectile", order = 2)]
public class SO_Projectile : ScriptableObject
{
    [Header("Proj Key Compontents")]
    public string ProjectileName;
    public string ProjectileDescription;
    public int Damage;
    public int ProjID;

    public enum ProjType
    {
        Throwable,
        Shootable
    }
    [Header("Proj Type")]
    public ProjType EProjType;

    public enum ProjUseType
    {
        Single,
        Charge
    }
    [Header("Proj Use Type")]
    public ProjUseType EProjUse;

    [Header("Proj Bullet Stats")]
    [Range(0f, 15f)]
    public float AOERange;
    public float Cooldown;
    public float KnockBackRange;
    public float ShootDistance;
    public Vector2 ClipSizeOnPickup;

    [Header("Proj Usage Vars")]
    public int TimeToUse;
    public int ShotSpeed;
    public int ProjPerShot;
    public int ClipSize;
    public int MaxReserveSize;

    [Header("Proj Audio")]
    public AudioClip[] ProjAudio;

    [Header("Proj Prefabs")]
    public GameObject ProjBulletPrefab;
    public GameObject ProjPrefab;
}
