using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weaponry/Melee", order = 3)]
public class SO_Melee : ScriptableObject
{
    public string MeleeName;
    public string MeleeDescription;

    public enum MeleeUseType
    {
        Single,
        Hold,
    }
    public MeleeUseType EMeleeType;

    [Range(0f, 15f)]
    public float AOERange;
    public float Cooldown;
    public float KnockBackRange;

    public int Damage;
    public int MeleeID;
    public int ShotAmount;
    public int UseSpeed;

    public bool ShootOut;

    public AudioClip[] MeleeAudio;
    public AudioSource MeleeSource;

    public GameObject MeleePrefab;
}
