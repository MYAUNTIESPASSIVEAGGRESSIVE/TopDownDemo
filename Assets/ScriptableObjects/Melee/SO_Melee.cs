using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weaponry/Melee", order = 3)]
public class SO_Melee : MonoBehaviour
{
    public string MeleeName;
    public string MeleeDescription;

    [Range(0f, 15f)]
    public float AOERange;

    public float Cooldown;

    public int Damage;

    public int MeleeID;

    public GameObject MeleePrefab;
}
