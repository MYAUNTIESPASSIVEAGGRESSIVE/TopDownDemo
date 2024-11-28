using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weaponry/Melee", order = 2)]
public class SO_Melee : ScriptableObject
{
    [Header("Melee Key Components")]
    public string MeleeName;
    public string MeleeDescription;
    public int Damage;
    public int MeleeID;

    public enum MeleeUseType
    {
        Single,
        Hold,
    }
    [Header("Melee Use Type")]
    public MeleeUseType EMeleeType;

    [Header("Melee Use Stats")]
    [Range(0f, 15f)]
    public float AOERange;
    public float Cooldown;
    public float KnockBackRange;
    public int ShotAmount;
    public int UseSpeed;
    public bool Gore;

    [Header("Melee Audio")]
    public AudioClip[] MeleeAudio;

    [Header("Melee Prefab")]
    public GameObject MeleePrefab;
}
