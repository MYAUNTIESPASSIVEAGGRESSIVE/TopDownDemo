using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Weaponry/Projectile", order = 2)]
public class SO_Projectile : ScriptableObject
{
    public string ProjectileName;
    public string ProjectileDescription;

    public enum ProjType
    {
        Throwable,
        Shootable
    }
    public ProjType EProjType;

    public enum ProjUseType
    {
        Single,
        Charge
    }
    public ProjUseType EProjUse;

    [Range(0f, 15f)]
    public float AOERange;
    public float Cooldown;
    public float KnockBackRange;
    public float ShootDistance;

    public int TimeToUse;
    public int ShotSpeed;
    public int Damage;
    public int ProjID;
    public int ProjPerShot;

    public AudioClip[] ProjAudio;
    public AudioSource ProjSource;

    public GameObject ProjBulletPrefab;
    public GameObject ProjPrefab;
}
