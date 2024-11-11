using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regular Gun", menuName = "Weaponry/Regular Gun", order = 1)]
public class SO_RegularGuns : ScriptableObject
{
    public string GunName;
    public string GunDescription;

    public enum EFireType
    {
        Single,
        Rapid,
    }
    public EFireType GunFireType;

    public int FireRate;

    public int ClipSize;

    public int GunID;

    public int Damage;

    public GameObject BulletPrefab;
}
