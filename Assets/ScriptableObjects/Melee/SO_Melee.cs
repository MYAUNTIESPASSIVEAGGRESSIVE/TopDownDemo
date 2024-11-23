using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weaponry/Melee", order = 3)]
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
    public bool ShootOut;

    [Header("Melee Audio")]
    public AudioClip[] MeleeAudio;
    public AudioSource MeleeSource;

    [Header("Melee Prefab")]
    public GameObject MeleePrefab;
}
