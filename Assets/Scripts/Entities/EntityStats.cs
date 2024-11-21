using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    protected CircleCollider2D EntityCol;
    protected Animator EntityAnim;
    protected Rigidbody2D EntityRB;

    public float MaxHealth;
    protected float CurrentHealth;
    internal bool IsDead;

    public int DEF;
    public float DmgRedPerPt = 0.05f;

    protected virtual void Start()
    {
        TryGetComponent(out EntityCol);
        TryGetComponent(out EntityAnim);
        TryGetComponent(out EntityRB);

        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDmg(float Dmg)
    {
        CurrentHealth -= Dmg * (1 - (DEF * DmgRedPerPt));

        if (CurrentHealth <= 0)
        {
            DeathLogic();
        }
    }

    protected virtual void DeathLogic()
    {
        
    }
}
