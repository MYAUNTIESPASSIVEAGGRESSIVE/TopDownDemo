using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weaponry/Melee", order = 3)]
public class SO_Melee : ScriptableObject
{
    public string MeleeName;
    public string MeleeDescription;

    [Range(0f, 15f)]
    public float AOERange;

    public float Cooldown;

    public int Damage;

    public int MeleeID;

    public Sprite MeleeSprite;
}
