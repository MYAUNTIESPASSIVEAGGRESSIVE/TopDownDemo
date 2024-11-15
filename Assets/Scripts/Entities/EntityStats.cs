using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityStats : MonoBehaviour
{
    protected NavMeshAgent MyNav;
    protected CircleCollider2D MyCol;
    protected Animator MyAnim;

    public float MaxHealth;
    protected float CurrentHealth;
    internal bool IsDead;

    public int DEF;
    public float DmgRedPerPt = 0.05f;

    protected virtual void Start()
    {
        TryGetComponent(out MyNav);
        TryGetComponent(out MyCol);
        TryGetComponent(out MyAnim);

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
