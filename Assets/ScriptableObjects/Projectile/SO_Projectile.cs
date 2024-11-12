using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Weaponry/Projectile", order = 2)]
public class SO_Projectile : ScriptableObject
{
    public string ProjectileName;
    public string ProjectileDescription;

    public enum EProjType
    {
        Throwable,
        Shootable,
    }
    public EProjType ProjectileType;

    [Range(0f, 15f)]
    public float AOERange;

    public float Cooldown;

    public int Damage;

    public int ProjID;

    public Sprite ProjSprite;

    public GameObject ProjPrefab;
}
