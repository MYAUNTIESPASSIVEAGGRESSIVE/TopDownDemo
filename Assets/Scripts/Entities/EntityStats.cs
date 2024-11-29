using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    protected ParticleSystem GoreDeathParticle;
    protected GameManager GameManagerScript;

    public float MaxHealth;
    public float CurrentHealth;
    internal bool IsDead;

    public int DEF;
    public float DmgRedPerPt = 0.05f;

    protected virtual void Start()
    {
        TryGetComponent(out GoreDeathParticle);


        CurrentHealth = MaxHealth;
    }

    protected virtual void Update()
    {
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public virtual void TakeDamage(float Dmg, bool GoryDeath)
    {
        CurrentHealth -= Dmg * (1 - (DEF * DmgRedPerPt));

        if (CurrentHealth <= 0)
        {
            EntityDeath(GoryDeath);
        }
    }

    protected virtual void EntityDeath(bool GoryDeath)
    {
        if (GoryDeath)
        {
            GoreDeathParticle.Play();
        }
    }
}
