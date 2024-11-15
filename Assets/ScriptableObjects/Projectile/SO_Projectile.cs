using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Weaponry/Projectile", order = 2)]
public class SO_Projectile : ScriptableObject
{
    public string ProjectileName;
    public string ProjectileDescription;

    [Range(0f, 15f)]
    public float AOERange;
    public float Cooldown;
    public int Damage;
    public int ProjID;

    public AudioClip ProjAudio;
    public AudioSource ProjSource;

    public GameObject ProjBulletPrefab;
    public GameObject ProjPrefab;
}
