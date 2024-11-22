using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float Dmg)
    {
        if (IsDead) return;

        base.TakeDamage(Dmg);
    }

    protected override void EntityDeath()
    {
        if (IsDead) return;

        CurrentHealth = 0;
        transform.GetComponent<PlayerControl>().enabled = false;
        transform.GetComponent<PlayerWeaponSelect>().enabled = false;
        IsDead = true;

    }
}
