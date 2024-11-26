using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : EntityStats
{
    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float Dmg, bool Gore)
    {
        if (IsDead) return;

        base.TakeDamage(Dmg, Gore);
    }

    protected override void EntityDeath(bool Gore)
    {
        if (IsDead) return;

        CurrentHealth = 0;
        transform.GetComponent<PlayerControl>().enabled = false;
        transform.GetComponent<PlayerWeaponSelect>().enabled = false;
        IsDead = true;

    }
}
